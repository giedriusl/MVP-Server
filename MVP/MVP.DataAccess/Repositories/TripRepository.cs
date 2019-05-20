using Microsoft.EntityFrameworkCore;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVP.DataAccess.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly MvpContext _context;

        public TripRepository(MvpContext context)
        {
            _context = context;
        }

        public async Task<Trip> AddTripAsync(Trip trip)
        {
            var tripEntity = _context.Trips.Add(trip).Entity;
            await _context.SaveChangesAsync();

            return tripEntity;
        }

        public async Task<IEnumerable<Trip>> GetAllTripsAsync()
        {
            var trips = await _context.Trips
                .Include(trip => trip.FlightInformations)
                .Include(trip => trip.RentalCarInformations)
                .Include(trip => trip.FromOffice)
                    .ThenInclude(fromOffice => fromOffice.Location)
                .Include(trip => trip.FromOffice)
                    .ThenInclude(fromOffice => fromOffice.Apartments)
                .Include(trip => trip.ToOffice)
                    .ThenInclude(toOffice => toOffice.Location)
                .Include(trip => trip.ToOffice)
                    .ThenInclude(toOffice => toOffice.Apartments)
                .Include(trip => trip.UserTrips)
                    .ThenInclude(userTrips => userTrips.User)
                .ToListAsync();

            return trips;
        }

        public async Task<Trip> GetTripByIdAsync(int tripId)
        {
            var trips = await _context.Trips
                .Include(trip => trip.FlightInformations)
                .Include(trip => trip.RentalCarInformations)
                .Include(trip => trip.FromOffice)
                    .ThenInclude(fromOffice => fromOffice.Location)
                .Include(trip => trip.FromOffice)
                    .ThenInclude(fromOffice => fromOffice.Apartments)
                .Include(trip => trip.ToOffice)
                    .ThenInclude(toOffice => toOffice.Location)
                .Include(trip => trip.ToOffice)
                    .ThenInclude(toOffice => toOffice.Apartments)
                .Include(trip => trip.UserTrips)
                    .ThenInclude(userTrips => userTrips.User)
                .FirstOrDefaultAsync(trip => trip.Id == tripId);
            
            return trips;
        }

        public async Task<IEnumerable<Trip>> GetTripsByUserIdAsync(string userId)
        {
            var lists = await _context.Trips
                .Where(trip => trip.UserTrips.Any(userTrip => userTrip.UserId == userId))
                .Include(trip => trip.FlightInformations)
                .Include(trip => trip.RentalCarInformations)
                .Include(trip => trip.FromOffice)
                    .ThenInclude(fromOffice => fromOffice.Location)
                .Include(trip => trip.FromOffice)
                    .ThenInclude(fromOffice => fromOffice.Apartments)
                .Include(trip => trip.ToOffice)
                    .ThenInclude(toOffice => toOffice.Location)
                .Include(trip => trip.ToOffice)
                    .ThenInclude(toOffice => toOffice.Apartments)
                .Include(trip => trip.UserTrips)
                    .ThenInclude(userTrip => userTrip.User)
                .ToListAsync();

            return lists;
        }

        public async Task<IEnumerable<User>> GetUsersByTripIdAsync(int tripId)
        {
            var users = await _context.Users.Where(user => user.UserTrips.Any(userTrip => userTrip.TripId == tripId))
                .Include(user => user.Calendars)
                .ToListAsync();

            return users;
        }

        public async Task<Trip> UpdateTripAsync(Trip trip)
        {
            var tripEntity = _context.Trips.Update(trip).Entity;
            await _context.SaveChangesAsync();

            return tripEntity;
        }

        public async Task DeleteTripAsync(Trip trip)
        {
            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();
        }
    }
}
