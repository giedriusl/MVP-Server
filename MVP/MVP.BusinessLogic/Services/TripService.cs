﻿using Microsoft.AspNetCore.Identity;
using MVP.BusinessLogic.Interfaces;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Dtos.Trips;
using MVP.Entities.Dtos.Users;
using MVP.Entities.Entities;
using MVP.Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVP.Entities.Dtos.FlightsInformation;
using MVP.Entities.Dtos.RentalCarsInformation;
using MVP.Entities.Enums;

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

                var tripEntity = await _tripRepository.AddTripAsync(trip);

                var usersInTrip = _userManager.Users.Where(user => createTripDto.UserIds.Contains(user.Id)).ToList();
                usersInTrip.ForEach(user => userTrips.Add(new UserTrip { TripId = tripEntity.Id, UserId = user.Id }));
                trip.UserTrips = userTrips;

                await _userTripRepository.AddUserTripsAsync(userTrips);

                return CreateTripDto.ToDto(tripEntity);
            }
            catch (Exception exception)
            {
                throw new BusinessLogicException(exception, "Failed to create trip");
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

        public async Task<IEnumerable<TripDto>> GetAllTripsAsync()
        {
            try
            {
                var trips = await _tripRepository.GetAllTripsAsync();
                var tripsDto = trips.Select(TripDto.ToDto).ToList();

                return tripsDto;
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

        public async Task<IEnumerable<TripDto>> GetTripsByUserIdAsync(string userId)
        {
            try
            {
                var trips = await _tripRepository.GetTripsByUserIdAsync(userId);
                var tripsDto = trips.Select(TripDto.ToDto).ToList();

                return tripsDto;
            }
            catch (Exception exception)
            {
                throw new BusinessLogicException(exception, "Failed to get trips");
            }
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
            try
            {
                var baseTrip = await _tripRepository.GetTripByIdAsync(baseTripId);
                var additionalTrip = await _tripRepository.GetTripByIdAsync(additionalTripId);

                ValidateTripsForMerge(baseTrip,additionalTrip);

                baseTrip.FlightInformations.AddRange(additionalTrip.FlightInformations);
                baseTrip.RentalCarInformations.AddRange(additionalTrip.RentalCarInformations);

                var mergedTrip = MergedTripDto.ToDto(baseTrip);
                mergedTrip.AdditionalTripId = additionalTripId;

                var users = (await _tripRepository.GetUsersByTripIdAsync(additionalTripId)).Select(UserDto.ToDto).ToList();
                RemoveDuplicateUsers(mergedTrip, users);
                mergedTrip.Users.AddRange(users);

                return mergedTrip;
            }
            catch (Exception exception)
            {
                throw new BusinessLogicException(exception, "Failed to get merge trips data");
            }
        }

        public async Task<CreateTripDto> MergeTripsAsync(MergedTripDto mergedTripDto)
        {
            try
            {
                var baseTrip = await _tripRepository.GetTripByIdAsync(mergedTripDto.BaseTripId);

                if (baseTrip is null)
                {
                    throw new BusinessLogicException("Base trip was not found.");
                }

                var toOffice = baseTrip.ToOffice;
                var fromOffice = baseTrip.FromOffice;

                var createTripDto = MergedTripDto.ToCreateTripDto(mergedTripDto, toOffice.Id, fromOffice.Id);
                var createdTrip = await CreateTripAsync(createTripDto);

                await _tripRepository.DeleteTripAsync(baseTrip);
                await DeleteTripAsync(mergedTripDto.AdditionalTripId);

                return createdTrip;
            }
            catch (Exception exception)
            {
                throw new BusinessLogicException(exception, "Failed to merge trips");
            }
        }

        public async Task<IEnumerable<TripViewDto>> GetSimilarTripsAsync(int tripId)
        {
            try
            {
                var trip = await _tripRepository.GetTripByIdAsync(tripId);

                if (trip is null)
                {
                    throw new BusinessLogicException("Trip is not found");
                }

                var similarTrips = await _tripRepository.GetSimilarTrips(trip);

                return similarTrips.Select(TripViewDto.ToDto);
            }
            catch (Exception exception)
            {
                throw new BusinessLogicException(exception, "Failed to get similar trips");
            }
        }

        private static void RemoveDuplicateUsers(MergedTripDto mergedTrip, ICollection<UserDto> users)
        {
            var duplicateUsers = users.Where(user => mergedTrip.Users.Select(u => u.Email).Contains(user.Email)).ToList();

            foreach (var duplicateUser in duplicateUsers)
            {
                users.Remove(duplicateUser);
            }
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

        private void ValidateCreateTrip(CreateTripDto createTripDto)
        {
            if (createTripDto.FromOfficeId == createTripDto.ToOfficeId)
            {
                throw new BusinessLogicException("Office from and office to cannot be the same!");
            }

            if (!_userManager.Users.Any(user => createTripDto.UserIds.Contains(user.Id)))
            {
                throw new BusinessLogicException("Trip should contain at least one user!");
            }
        }
    }
}
