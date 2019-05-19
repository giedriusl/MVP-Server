using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVP.DataAccess.Interfaces
{
    public interface ITripRepository
    {
        Task<Trip> AddTripAsync(Trip trip);
        Task<Trip> UpdateTripAsync(Trip trip);

        Task<IEnumerable<Trip>> GetAllTripsAsync();
        Task<Trip> GetTripByIdAsync(int tripId);
        Task<IEnumerable<Trip>> GetTripsByUserId(string userId);
        Task<IEnumerable<User>> GetUsersByTripId(int tripId);
    }
}
