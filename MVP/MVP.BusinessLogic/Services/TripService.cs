using Microsoft.AspNetCore.Identity;
using MVP.BusinessLogic.Interfaces;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Dtos.FlightsInformation;
using MVP.Entities.Dtos.RentalCarsInformation;
using MVP.Entities.Dtos.Trips;
using MVP.Entities.Dtos.Users;
using MVP.Entities.Entities;
using MVP.Entities.Enums;
using MVP.Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVP.BusinessLogic.Helpers.UrlBuilder;
using MVP.EmailService.Interfaces;
using MVP.Entities.Dtos.Apartments.ApartmentRooms;

namespace MVP.BusinessLogic.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IUserTripRepository _userTripRepository;
        private readonly ICalendarRepository _calendarRepository;
        private readonly UserManager<User> _userManager;
        private readonly IEmailManager _emailManager;
        private readonly IUrlBuilder _urlBuilder;
        private readonly ITripApartmentInfoRepository _tripApartmentInfoRepository;


        public TripService(ITripRepository tripRepository, 
            IOfficeRepository officeRepository, 
            UserManager<User> userManager, 
            IUserTripRepository userTripRepository, 
            IEmailManager emailManager, 
            IUrlBuilder urlBuilder,
            IApartmentRepository apartmentRepository, 
            ICalendarRepository calendarRepository, 
            ITripApartmentInfoRepository tripApartmentInfoRepository)
        {
            _tripRepository = tripRepository;
            _officeRepository = officeRepository;
            _userManager = userManager;
            _userTripRepository = userTripRepository;
            _emailManager = emailManager;
            _urlBuilder = urlBuilder;
            _apartmentRepository = apartmentRepository;
            _calendarRepository = calendarRepository;
            _tripApartmentInfoRepository = tripApartmentInfoRepository;
        }

        public async Task<CreateTripDto> CreateTripAsync(CreateTripDto createTripDto)
        {
            ValidateCreateTrip(createTripDto);

            var userTrips = new List<UserTrip>();
            var trip = CreateTripDto.ToEntity(createTripDto);
            var fromOffice = await _officeRepository.GetOfficeByIdAsync(createTripDto.FromOfficeId);

            if (fromOffice is null)
            {
                throw new BusinessLogicException("Office from not found", "officeNotFound");
            }

            var toOffice = await _officeRepository.GetOfficeByIdAsync(createTripDto.ToOfficeId);

            if (toOffice is null)
            {
                throw new BusinessLogicException("Office to not found", "officeNotFound");
            }

            trip.FromOffice = fromOffice;
            trip.ToOffice = toOffice;

            var tripEntity = await _tripRepository.AddTripAsync(trip);

                var usersInTrip = _userManager.Users
                    .Where(user => createTripDto.UserIds.Contains(user.Id))
                    .Select(u => new { u.Id, u.Email })
                    .ToList();

                foreach (var user in usersInTrip)
                {
                    userTrips.Add(new UserTrip
                    {
                        TripId = tripEntity.Id, UserId = user.Id
                    });

                    SendConfirmationEmail(user.Email, tripEntity.Id);
                }

                trip.UserTrips = userTrips;

            await _userTripRepository.AddUserTripsAsync(userTrips);

            return CreateTripDto.ToDto(tripEntity);
        }

        public async Task DeleteTripAsync(int tripId)
        {
            var existingTrip = await _tripRepository.GetTripByIdAsync(tripId);

            if (existingTrip is null)
            {
                throw new BusinessLogicException("Trip  was not found", "tripNotFound");
            }

            await _tripRepository.DeleteTripAsync(existingTrip);
        }

        public async Task<IEnumerable<TripDto>> GetAllTripsAsync()
        {
            var trips = await _tripRepository.GetAllTripsAsync();
            var tripsDto = trips.Select(TripDto.ToDto).ToList();

            return tripsDto;
        }

        public async Task<TripViewDto> GetTripByIdAsync(int tripId)
        {
            var trip = await _tripRepository.GetTripByIdAsync(tripId);

            if (trip is null)
            {
                throw new BusinessLogicException("Trip was not found", "tripNotFound");
            }

            return TripViewDto.ToDto(trip);
        }

        public async Task<IEnumerable<TripDto>> GetTripsByUserIdAsync(string userId)
        {
            var trips = await _tripRepository.GetTripsByUserIdAsync(userId);
            var tripsDto = trips.Select(TripDto.ToDto).ToList();

            return tripsDto;
        }

        public IEnumerable<TripStatusDto> GetTripStatuses()
        {
            var statuses = Enum.GetValues(typeof(TripStatus)).Cast<TripStatus>();

            return statuses.Select(TripStatusDto.ToDto).ToList();
        }

        public IEnumerable<RentalCarStatusDto> GetRentalCarStatuses()
        {
            var statuses = Enum.GetValues(typeof(RentalCarStatus)).Cast<RentalCarStatus>();

            return statuses.Select(RentalCarStatusDto.ToDto).ToList();
        }

        public IEnumerable<FlightInformationStatusDto> GetFlightInformationStatuses()
        {
            var statuses = Enum.GetValues(typeof(FlightInformationStatus)).Cast<FlightInformationStatus>();

            return statuses.Select(FlightInformationStatusDto.ToDto).ToList();
        }

        public async Task ConfirmAsync(int tripId, string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null)
            {
                throw new BusinessLogicException("User was not found");
            }

            var trip = await _tripRepository.GetTripByIdAsync(tripId);
            if (trip is null)
            {
                throw new BusinessLogicException("Trip was not found");
            }

            var usersInTrip = trip.UserTrips.Select(ut => ut.User);
            if (!usersInTrip.Contains(user))
            {
                throw new BusinessLogicException("User is not in this trip.");
            }

            var userTrip = trip.UserTrips.First(ut => ut.UserId == user.Id);
            userTrip.Confirmed = true;
            await _userTripRepository.UpdateUserTripAsync(userTrip);
        }

        public async Task<MergedTripDto> GetMergedTripsDataAsync(int baseTripId, int additionalTripId)
        {
            var baseTrip = await _tripRepository.GetTripByIdAsync(baseTripId);
            var additionalTrip = await _tripRepository.GetTripByIdAsync(additionalTripId);

            ValidateTripsForMerge(baseTrip, additionalTrip);

            baseTrip.FlightInformations.AddRange(additionalTrip.FlightInformations);
            baseTrip.RentalCarInformations.AddRange(additionalTrip.RentalCarInformations);

            var mergedTrip = MergedTripDto.ToDto(baseTrip);
            mergedTrip.AdditionalTripId = additionalTripId;

            var users = (await _tripRepository.GetUsersByTripIdAsync(additionalTripId)).Select(UserDto.ToDto).ToList();
            var uniqueUsers = RemoveDuplicateUsers(mergedTrip, users);

            mergedTrip.Users.AddRange(uniqueUsers);

            return mergedTrip;
        }

        public async Task<CreateTripDto> MergeTripsAsync(MergedTripDto mergedTripDto)
        {
            var baseTrip = await _tripRepository.GetTripByIdAsync(mergedTripDto.BaseTripId);

            if (baseTrip is null)
            {
                throw new BusinessLogicException("Base trip was not found.", "tripNotFound");
            }

            var toOffice = baseTrip.ToOffice;
            var fromOffice = baseTrip.FromOffice;

            var createTripDto = MergedTripDto.ToCreateTripDto(mergedTripDto, toOffice.Id, fromOffice.Id);
            var createdTrip = await CreateTripAsync(createTripDto);

            await _tripRepository.DeleteTripAsync(baseTrip);
            await DeleteTripAsync(mergedTripDto.AdditionalTripId);

            return createdTrip;
        }

        public async Task<IEnumerable<TripViewDto>> GetSimilarTripsAsync(int tripId)
        {
            var trip = await _tripRepository.GetTripByIdAsync(tripId);

            if (trip is null)
            {
                throw new BusinessLogicException("Trip is not found", "tripNotFound");
            }

            var similarTrips = await _tripRepository.GetSimilarTrips(trip);

            return similarTrips.Select(TripViewDto.ToDto);
        }

        public async Task AddFlightInformationToTripAsync(int tripId,
            FlightInformationDto flightInformationDto)
        {
            var trip = await _tripRepository.GetTripByIdWithFlightInformationAsync(tripId);

            if (trip is null)
            {
                throw new BusinessLogicException("Trip was not found", "tripNotFound");
            }

            trip.FlightInformations.Add(FlightInformationDto.ToEntity(flightInformationDto));
            await _tripRepository.UpdateTripAsync(trip);
        }

        public async Task DeleteFlightInformationFromTripAsync(int tripId, int flightInformationId)
        {
            var trip = await _tripRepository.GetTripByIdWithFlightInformationAsync(tripId);

            if (trip is null)
            {
                throw new BusinessLogicException("Trip does not exist", "tripNotFound");
            }

            if (trip.FlightInformations.Count == 0)
            {
                throw new BusinessLogicException("Specified flight information does not exist", "flightInfoNotFound");
            }

            var flightInformationToRemove = trip.FlightInformations
                .First(flightInformation => flightInformation.Id == flightInformationId);

            if (flightInformationToRemove is null)
            {
                throw new BusinessLogicException("Specified flight information does not exist", "flightInfoNotFound");
            }

            trip.FlightInformations.Remove(flightInformationToRemove);
            await _tripRepository.UpdateTripAsync(trip);
        }

        public async Task AddRentalCarInformationToTripAsync(int tripId,
            RentalCarInformationDto rentalCarInformationDto)
        {
            var trip = await _tripRepository.GetTripByIdWithRentalCarInformationAsync(tripId);

            if (trip is null)
            {
                throw new BusinessLogicException("Trip was not found", "tripNotFound");
            }

            trip.RentalCarInformations.Add(RentalCarInformationDto.ToEntity(rentalCarInformationDto));
            await _tripRepository.UpdateTripAsync(trip);
        }

        public async Task UpdateRentalCarInformationForTripAsync(int tripId,
            UpdateRentalCarInformationDto updateRentalCarInformationDto)
        {
            ValidateRentalCarInformation(updateRentalCarInformationDto);
            var trip = await _tripRepository.GetTripByIdAsync(tripId);

            if (trip is null)
            {
                throw new BusinessLogicException("Trip was not found", "tripNotFound");
            }

            var rentalCarInformationToUpdate = trip.RentalCarInformations
                .First(rentalCarInformation => rentalCarInformation.Id == updateRentalCarInformationDto.Id);

            if (rentalCarInformationToUpdate is null)
            {
                throw new BusinessLogicException("Specified rental car information does not exist", "rentalCarInfoNotFound");
            }

            rentalCarInformationToUpdate.UpdateRentalCarInformation(updateRentalCarInformationDto);
            await _tripRepository.UpdateTripAsync(trip);
        }

        public async Task DeleteRentalCarInformationFromTripAsync(int tripId, int rentalCarInformationId)
        {
            var trip = await _tripRepository.GetTripByIdWithRentalCarInformationAsync(tripId);

            if (trip is null)
            {
                throw  new BusinessLogicException("Trip was not found", "tripNotFound");
            }

            var rentalCarInformationToDelete = trip.RentalCarInformations
                .First(rentalCarInformation => rentalCarInformation.Id == rentalCarInformationId);

            if (rentalCarInformationToDelete is null)
            {
                throw new BusinessLogicException("Rental car information not found", "rentalCarInfoNotFound");
            }

            trip.RentalCarInformations.Remove(rentalCarInformationToDelete);
            await _tripRepository.UpdateTripAsync(trip);
        }

        public async Task UpdateTripAsync(int tripId, CreateTripDto updateTripDto)
        {
            ValidateCreateTrip(updateTripDto);
            var trip = await _tripRepository.GetTripByIdAsync(tripId);

            if (trip is null)
            {
                throw new BusinessLogicException("Trip was not found", "tripNotFound");
            }

            var originalUsers = trip.UserTrips.Select(userTrip => userTrip.UserId).ToList();
            var updateUsers = updateTripDto.UserIds;
            var areAllEqual = originalUsers.All(updateUsers.Contains) && originalUsers.Count == updateUsers.Count;

            if (!areAllEqual)
            {
                var userTripsToAdd = new List<UserTrip>();
                var userTrips = await _userTripRepository.GetUserTripsByTripIdAsync(trip.Id);

                var usersToDelete = originalUsers.Where(original => updateUsers.All(update => update != original)).ToList();
                var usersToAdd = updateUsers.Where(update => originalUsers.All(orig => orig != update)).ToList();

                var userTripsToDelete = userTrips.Where(userTrip => usersToDelete.Contains(userTrip.UserId)).ToList();
                usersToAdd.ForEach(add => userTripsToAdd.Add(new UserTrip { TripId = trip.Id, UserId = add }));

                await _userTripRepository.DeleteUserTripsAsync(userTripsToDelete);
                await _userTripRepository.AddUserTripsAsync(userTripsToAdd);
            }

            trip.UpdateTrip(updateTripDto);

            await _tripRepository.UpdateTripAsync(trip);
        }

        public async Task UpdateFlightInformationForTripAsync(int tripId,
            UpdateFlightInformationDto updateFlightInformationDto)
        {
            ValidateUpdateFlightInformation(updateFlightInformationDto);
            var trip = await _tripRepository.GetTripByIdAsync(tripId);

            if (trip is null)
            {
                throw new BusinessLogicException("Trip not found", "tripNotFound");
            }

            var flightInformationToUpdate = trip.FlightInformations
                .FirstOrDefault(flightInformation => flightInformation.Id == updateFlightInformationDto.Id);

            if (flightInformationToUpdate is null)
            {
                throw new BusinessLogicException("Specified flight information does not exist", "flightInfoNotFound");
            }

            flightInformationToUpdate.UpdateFlightInformation(updateFlightInformationDto);
            await _tripRepository.UpdateTripAsync(trip);
        }

        public async Task<IEnumerable<UserDto>> GetTripUsers(int tripId)
        {
            var userIds = await _userTripRepository.GetTripUserIdsByTripIdAsync(tripId);
            var users = _userManager.Users.Where(user => userIds.Contains(user.Id)).ToList();

            return users.Select(UserDto.ToDto);
        }

        public async Task<IEnumerable<FlightInformationDto>> GetTripsFlightInformationsAsync(int tripId)
        {
            var informations = await _tripRepository.GetTripsFlightInformationsByTripIdAsync(tripId);

            return informations.Select(FlightInformationDto.ToDto);
        }

        public async Task<IEnumerable<RentalCarInformationDto>> GetTripsRentalCarInformationsAsync(int tripId)
        {
            var informations = await _tripRepository.GetTripsRentalCarInformationsByTripIdAsync(tripId);

            return informations.Select(RentalCarInformationDto.ToDto);
        }

        public async Task<IEnumerable<CreateTripDto>> GetMergableTrips()
        {
            var trips = await _tripRepository.GetMergableTrips();

            return trips.Select(CreateTripDto.ToDto);
        }

        private static IEnumerable<UserDto> RemoveDuplicateUsers(MergedTripDto mergedTrip, ICollection<UserDto> users)
        {
            var duplicateUsers = users.Where(user => mergedTrip.Users.Select(u => u.Email).Contains(user.Email)).ToList();

            duplicateUsers.ForEach(duplicateUser => users.Remove(duplicateUser));

            return users;
        }

        private static void ValidateTripsForMerge(Trip baseTrip, Trip additionalTrip)
        {
            if (baseTrip is null || additionalTrip is null)
            {
                throw new BusinessLogicException("Trip was not found");
            }

            ValidateTripStatus(baseTrip);
            ValidateTripStatus(additionalTrip);
        }

        private static void ValidateTripStatus(Trip trip)
        {
            if (trip.TripStatus == TripStatus.InProgress || trip.TripStatus == TripStatus.Completed)
            {
                throw new BusinessLogicException($"One of the trips is in {trip.TripStatus} status so it cannot be merged.");
            }
        }

        public async Task<TripApartmentInfo> AddUsersToRooms(int tripId, UserToRoomDto userToRoom)
        {
            var apartment = _apartmentRepository.GetApartmentByIdAsync(userToRoom.ApartmentId);
            if (apartment is null)
            {
                throw new BusinessLogicException("Apartment not found", "apartmentNotFound");
            }

            var isRoomAvailable = await _apartmentRepository.IsRoomAvailable(userToRoom.ApartmentId, userToRoom.ApartmentRoomId, userToRoom.Start, userToRoom.End);
            if (!isRoomAvailable)
            {
                throw new BusinessLogicException("This room is not available at given time period.", "roomNotAvailable");
            }

            var user = _userManager.FindByIdAsync(userToRoom.UserId);
            if (user is null)
            {
                throw new BusinessLogicException("User not found", "userNotFound");
            }

            var calendar = await _calendarRepository.AddCalendarAsync(new Calendar
            {
                ApartmentRoomId = userToRoom.ApartmentRoomId,
                UserId = userToRoom.UserId,
                Start = userToRoom.Start,
                End = userToRoom.End
            });

            var tripApartmentInfo = await _tripApartmentInfoRepository.AddTripApartmentInfoAsync(new TripApartmentInfo
            {
                ApartmentRoomId = userToRoom.ApartmentRoomId,
                UserId = userToRoom.UserId,
                TripId = tripId,
                CalendarId = calendar.Id
            });

            return tripApartmentInfo;
        }

        private void ValidateCreateTrip(CreateTripDto createTripDto)
        {
            if (createTripDto.FromOfficeId == createTripDto.ToOfficeId)
            {
                throw new BusinessLogicException("Office from and office to cannot be the same!", "invalidOffices");
            }

            if (!_userManager.Users.Any(user => createTripDto.UserIds.Contains(user.Id)))
            {
                throw new BusinessLogicException("Trip should contain at least one user!", "noUsers");
            }

            if (createTripDto.End < createTripDto.Start)
            {
                throw new BusinessLogicException("Trip start date cannot be later than trip end date!", "invalidDateRage");
            }

            foreach (var flightInformation in createTripDto.FlightInformations)
            {
                if (flightInformation.End < flightInformation.Start)
                {
                    throw new BusinessLogicException($"Flight {flightInformation.Id} start date cannot be later than end date!", "invalidDateRage");
                }
            }

            foreach (var rentalCarInformation in createTripDto.RentalCarInformations)
            {
                if (rentalCarInformation.End < rentalCarInformation.Start)
                {
                    throw new BusinessLogicException($"Rental car {rentalCarInformation.Id} start date cannot be later than end date!", "invalidDateRage");
                }
            }
        }

        private void ValidateUpdateFlightInformation(UpdateFlightInformationDto updateFlightInformationDto)
        {
            if (updateFlightInformationDto.Start > updateFlightInformationDto.End)
            {
                throw new BusinessLogicException($"Flight information {updateFlightInformationDto.Id} start date cannot be later than end date!", "invalidDateRage");
            }
        }

        private void ValidateRentalCarInformation(RentalCarInformationDto rentalCarInformationDto)
        {
            if (rentalCarInformationDto.Start > rentalCarInformationDto.End)
            {
                throw new BusinessLogicException($"Rental car information {rentalCarInformationDto.Id} start date cannot be later than end date!", "invalidDateRage");
            }
        }

        private void SendConfirmationEmail(string email, int tripId)
        {
            var url = _urlBuilder.BuildTripConfirmationLink(tripId);
            _emailManager.SendTripConfirmationEmail(email, url);
        }
    }
}
