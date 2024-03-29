﻿using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVP.DataAccess.Interfaces
{
    public interface ITripRepository
    {
        Task<Trip> AddTripAsync(Trip trip);
        Task<Trip> UpdateTripAsync(Trip trip);
        Task DeleteTripAsync(Trip trip);

        Task<IEnumerable<Trip>> GetAllTripsAsync();
        Task<Trip> GetTripByIdAsync(int tripId);
        Task<Trip> GetTripByIdWithFlightInformationAsync(int tripId);
        Task<Trip> GetTripByIdWithRentalCarInformationAsync(int tripId);
        Task<IEnumerable<Trip>> GetTripsByUserIdAsync(string userId);
        Task<IEnumerable<Trip>> GetSimilarTrips(Trip trip);
        Task<IEnumerable<User>> GetUsersByTripIdAsync(int tripId);
        Task<IEnumerable<Trip>> GetTripsByOrganizerIdAsync(string organizerId);
        Task<IEnumerable<FlightInformation>> GetTripsFlightInformationsByTripIdAsync(int tripId);
        Task<IEnumerable<RentalCarInformation>> GetTripsRentalCarInformationsByTripIdAsync(int tripId);
        Task<IEnumerable<Trip>> GetMergableTrips();
    }
}
