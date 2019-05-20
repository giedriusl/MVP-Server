using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MVP.Entities.Entities;

namespace MVP.DataAccess.Interfaces
{
    public interface IUserTripRepository
    {
        Task<UserTrip> AddUserTripAsync(UserTrip userTrip);
        Task AddUserTripsAsync(List<UserTrip> userTrips);
        Task DeleteUserTripAsync(UserTrip userTrip);
    }
}
