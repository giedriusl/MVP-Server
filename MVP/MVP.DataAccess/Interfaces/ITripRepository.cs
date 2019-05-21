﻿using System;
using MVP.Entities.Entities;
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
        Task<IEnumerable<Trip>> GetTripsByUserIdAsync(string userId);
        Task<IEnumerable<Trip>> GetSimilarTrips(Trip trip);
        Task<IEnumerable<User>> GetUsersByTripIdAsync(int tripId);
    }
}
