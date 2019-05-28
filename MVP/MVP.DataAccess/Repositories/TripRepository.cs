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
                .Include(t => t.FromOffice)
                .Include(t => t.ToOffice)
                .ToListAsync();

            return trips;
        }

        public async Task<Trip> GetTripByIdAsync(int tripId)
        {
            var tripEntity = await _context.Trips
                .Include(trip => trip.FromOffice)
                    .ThenInclude(fromOffice => fromOffice.Location)
                .Include(trip => trip.ToOffice)
                    .ThenInclude(toOffice => toOffice.Location)
                .Include(trip => trip.UserTrips)
                    .ThenInclude(userTrips => userTrips.User)
                .FirstOrDefaultAsync(trip => trip.Id == tripId);

            return tripEntity;
        }

        public async Task<Trip> GetTripByIdWithFlightInformationAsync(int tripId)
        {
            var tripEntity = await _context.Trips
                .Include(trip => trip.FlightInformations)
                .FirstOrDefaultAsync(trip => trip.Id == tripId);

            return tripEntity;
        }

        public async Task<Trip> GetTripByIdWithRentalCarInformationAsync(int tripId)
        {
            var tripEntity = await _context.Trips
                .Include(trip => trip.RentalCarInformations)
                .FirstOrDefaultAsync(trip => trip.Id == tripId);

            return tripEntity;
        }

        public async Task<IEnumerable<Trip>> GetTripsByUserIdAsync(string userId)
        {
            var trips = await _context.Trips
                .Where(trip => trip.UserTrips.Any(userTrip => userTrip.UserId == userId))
                .Include(trip => trip.ToOffice)
                    .ThenInclude(office => office.Location)
                .Include(trip => trip.FromOffice)
                    .ThenInclude(office => office.Location)
                .ToListAsync();

            return trips;
        }

        public async Task<IEnumerable<Trip>> GetSimilarTrips(Trip trip)
        {
            var trips = await _context.Trips
                .Where(t => t.ToOfficeId == trip.ToOfficeId
                                && t.FromOfficeId == trip.FromOfficeId
                                && t.Start <= trip.Start.AddDays(1)
                                && t.Start >= trip.Start.AddDays(-1)
                                && t.End <= trip.End.AddDays(1)
                                && t.End >= trip.End.AddDays(-1)
                                && t.Id != trip.Id)
                .ToListAsync();

            return trips;
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

        public async Task<IEnumerable<FlightInformation>> GetTripsFlightInformationsByTripIdAsync(int tripId)
        {
            var informations = await _context.Trips
                .Where(trip => trip.Id == tripId)
                .SelectMany(trip => trip.FlightInformations)
                .ToListAsync();

            return informations;
        }

        public async Task<IEnumerable<RentalCarInformation>> GetTripsRentalCarInformationsByTripIdAsync(int tripId)
        {
            var informations = await _context.Trips
                .Where(trip => trip.Id == tripId)
                .SelectMany(trip => trip.RentalCarInformations)
                .ToListAsync();

            return informations;
        }
    }
}
