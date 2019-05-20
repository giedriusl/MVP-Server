using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MVP.BusinessLogic.Interfaces;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Dtos.Trips;
using MVP.Entities.Entities;
using MVP.Entities.Exceptions;

namespace MVP.BusinessLogic.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IUserTripRepository _userTripRepository;
        private readonly UserManager<User> _userManager;


        public TripService(ITripRepository tripRepository, IOfficeRepository officeRepository, UserManager<User> userManager, IUserTripRepository userTripRepository)
        {
            _tripRepository = tripRepository;
            _officeRepository = officeRepository;
            _userManager = userManager;
            _userTripRepository = userTripRepository;
        }

        public async Task<CreateTripDto> CreateTripAsync(CreateTripDto createTripDto)
        {
            try
            {
                ValidateCreateTrip(createTripDto);

                var userTrips = new List<UserTrip>();
                var trip = CreateTripDto.ToEntity(createTripDto);
                var fromOffice = await _officeRepository.GetOfficeByIdAsync(createTripDto.FromOfficeId);

                if (fromOffice is null)
                {
                    throw new BusinessLogicException("Office from not found");
                }

                var toOffice = await _officeRepository.GetOfficeByIdAsync(createTripDto.ToOfficeId);

                if (toOffice is null)
                {
                    throw new BusinessLogicException("Office to not found");
                }

                trip.FromOffice = fromOffice;
                trip.ToOffice = toOffice;

                var usersInTrip = _userManager.Users.Where(user => createTripDto.UserIds.Contains(user.Id)).ToList();
                usersInTrip.ForEach(user => userTrips.Add(new UserTrip{Trip = trip, User = user}));
                trip.UserTrips = userTrips;

                await _userTripRepository.AddUserTripsAsync(userTrips);
                var tripEntity = await _tripRepository.AddTripAsync(trip);

                return CreateTripDto.ToDto(tripEntity);
            }
            catch (Exception exception)
            {
                throw new BusinessLogicException(exception,"Failed to create trip");
            }

        }

        public async Task DeleteTripAsync(int tripId)
        {
            try
            {
                var existingTrip = await _tripRepository.GetTripByIdAsync(tripId);

                if (existingTrip is null)
                {
                    throw new BusinessLogicException("Trip  was not found");
                }

                await _tripRepository.DeleteTripAsync(existingTrip);
            }
            catch (Exception exception)
            {
                throw new BusinessLogicException(exception, "Failed to delete trip");
            }
        }

        public async Task<IEnumerable<TripViewDto>> GetAllTripsAsync()
        {
            try
            {
                var trips = await _tripRepository.GetAllTripsAsync();
                var tripsViewDto = trips.Select(TripViewDto.ToDto).ToList();

                return tripsViewDto;
            }
            catch (Exception exception)
            {
                throw new BusinessLogicException(exception, "Failed to get all trips");
            }
        }

        public async Task<TripViewDto> GetTripByIdAsync(int tripId)
        {
            try
            {
                var trip = await _tripRepository.GetTripByIdAsync(tripId);

                if (trip is null)
                {
                    throw new BusinessLogicException("Trip was not found");
                }

                return TripViewDto.ToDto(trip);
            }
            catch (Exception exception)
            {
                throw new BusinessLogicException(exception, "Failed to get trip");
            }
        }

        public async Task<IEnumerable<TripViewDto>> GetTripsByUserIdAsync(string userId)
        {
            try
            {
                var trips = await _tripRepository.GetTripsByUserIdAsync(userId);
                var tripsViewDto = trips.Select(TripViewDto.ToDto).ToList();

                return tripsViewDto;
            }
            catch (Exception exception)
            {
                throw new BusinessLogicException(exception, "Failed to get trips");
            }
        }

        private void ValidateCreateTrip(CreateTripDto createTripDto)
        {
            if (createTripDto.FromOfficeId == createTripDto.ToOfficeId)
            {
                throw new BusinessLogicException("Office from and office to cannot be the same!");
            }

            var users = _userManager.Users.Where(user => createTripDto.UserIds.Contains(user.Id)).ToList();

            if (!users.Any())
            {
                throw new BusinessLogicException("Trip should contain at least one user!");
            }
        }
    }
}
