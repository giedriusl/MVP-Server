using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;

namespace MVP.DataAccess.Repositories
{
    public class UserTripRepository :IUserTripRepository
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

        public async Task UpdateUserTripAsync(UserTrip userTrip)
        {
            _context.UserTrips.Update(userTrip);
            await _context.SaveChangesAsync();
        }
    }
}
