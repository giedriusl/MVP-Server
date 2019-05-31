using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVP.DataAccess.Interfaces
{
    public interface IUserTripRepository
    {
        Task<UserTrip> AddUserTripAsync(UserTrip userTrip);
        Task AddUserTripsAsync(List<UserTrip> userTrips);
        Task DeleteUserTripAsync(UserTrip userTrip);
        Task DeleteUserTripsAsync(List<UserTrip> userTrips);
        Task<List<UserTrip>> GetUserTripsByTripIdAsync(int tripId);
        Task<IEnumerable<string>> GetTripUserIdsByTripIdAsync(int tripId);
        Task UpdateUserTripAsync(UserTrip userTrip);
        Task<UserTrip> GetUserTripByTripIdAndUserEmailAsync(int tripId, string userEmail);
        Task<IEnumerable<Trip>> GetUnconfirmedTripsByUserEmailAsync(string userEmail);
    }
}
