using Microsoft.EntityFrameworkCore;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVP.DataAccess.Repositories
{
    public class UserTripRepository : IUserTripRepository
    {
        private readonly MvpContext _context;

        public UserTripRepository(MvpContext context)
        {
            _context = context;
        }

        public async Task<UserTrip> AddUserTripAsync(UserTrip userTrip)
        {
            var userTripEntity = _context.UserTrips.Add(userTrip).Entity;
            await _context.SaveChangesAsync();

            return userTripEntity;
        }

        public async Task AddUserTripsAsync(List<UserTrip> userTrips)
        {
            _context.UserTrips.AddRange(userTrips);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserTripAsync(UserTrip userTrip)
        {
            _context.UserTrips.Remove(userTrip);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserTripsAsync(List<UserTrip> userTrips)
        {
            _context.UserTrips.RemoveRange(userTrips);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserTrip>> GetUserTripsByTripIdAsync(int tripId)
        {
            var userTrips = await _context.UserTrips
                .Where(userTrip => userTrip.TripId == tripId)
                .ToListAsync();

            return userTrips;
        }

        public async Task<IEnumerable<string>> GetTripUserIdsByTripIdAsync(int tripId)
        {
            var userTrips = await _context.UserTrips
                .Where(userTrip => userTrip.TripId == tripId)
                .Select(userTrip => userTrip.UserId)
                .ToListAsync();

            return userTrips;
        }

        public async Task UpdateUserTripAsync(UserTrip userTrip)
        {
            _context.UserTrips.Update(userTrip);
            await _context.SaveChangesAsync();
        }

        public async Task<UserTrip> GetUserTripByTripIdAndUserEmailAsync(int tripId, string userEmail)
        {
            var userTrip = await _context.UserTrips
                .Include(ut => ut.Trip)
                .ThenInclude(t => t.FromOffice)
                .Include(ut => ut.Trip)
                .ThenInclude(t => t.ToOffice)
                .FirstOrDefaultAsync(ut => ut.TripId == tripId && ut.User.Email == userEmail);

            return userTrip;
        }

        public async Task<IEnumerable<Trip>> GetUnconfirmedTripsByUserEmailAsync(string userEmail)
        {
            var trips = await _context.UserTrips
                .Where(ut => ut.User.Email == userEmail && !ut.Confirmed)
                .Select(ut => ut.Trip)
                .Include(t => t.FromOffice)
                .Include(t => t.ToOffice)
                .ToListAsync();

            return trips;
        }
    }
}
