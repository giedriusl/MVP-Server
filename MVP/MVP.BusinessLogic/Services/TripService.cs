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

namespace MVP.BusinessLogic.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IUserTripRepository _userTripRepository;
        private readonly UserManager<User> _userManager;


        public TripService(ITripRepository tripRepository,
            IOfficeRepository officeRepository,
            UserManager<User> userManager,
            IUserTripRepository userTripRepository)
        {
            _tripRepository = tripRepository;
            _officeRepository = officeRepository;
            _userManager = userManager;
            _userTripRepository = userTripRepository;
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

            var usersInTrip = _userManager.Users.Where(user => createTripDto.UserIds.Contains(user.Id)).ToList();
            usersInTrip.ForEach(user => userTrips.Add(new UserTrip { TripId = tripEntity.Id, UserId = user.Id }));
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
                throw new BusinessLogicException("Trip was not found", "tripNotFound");
            }

            ValidateTripStatus(baseTrip);
            ValidateTripStatus(additionalTrip);
        }

        private static void ValidateTripStatus(Trip trip)
        {
            if (trip.TripStatus == TripStatus.InProgress || trip.TripStatus == TripStatus.Completed)
            {
                throw new BusinessLogicException($"One of the trips is in {trip.TripStatus} status so it cannot be merged.", "invalidTripStatus");
            }
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
        }
    }
}
