using System;
using Microsoft.EntityFrameworkCore;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVP.DataAccess.Repositories
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly MvpContext _context;

        public ApartmentRepository(MvpContext context)
        {
            _context = context;
        }

        public async Task<Apartment> AddApartmentAsync(Apartment apartment)
        {
            var apartmentEntity =  _context.Apartments.Add(apartment).Entity;
            await _context.SaveChangesAsync();

            return apartmentEntity;
        }

        public async Task UpdateApartmentAsync(Apartment apartment)
        {
             _context.Apartments.Update(apartment);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteApartmentAsync(Apartment apartment)
        {
            _context.Apartments.Remove(apartment);
            await _context.SaveChangesAsync();
        }
        public async Task<Apartment> GetApartmentByIdAsync(int apartmentId)
        {
            var apartment = await _context.Apartments
                .Include(a => a.Location)
                .FirstOrDefaultAsync(x => x.Id == apartmentId);

            return apartment;
        }

        public async Task<Apartment> GetApartmentWithRoomsByIdAsync(int apartmentId)
        {
            var apartment = await _context.Apartments
                .Include(a => a.Rooms)
                .FirstOrDefaultAsync(a => a.Id == apartmentId);

            return apartment;
        }

        public async Task<List<ApartmentRoom>> GetApartmentRoomsByNumberAsync(int apartmentId, List<int> roomNumbers)
        {
            var apartment = await _context.Apartments
                .Where(a => a.Id == apartmentId)
                .SelectMany(r => r.Rooms)
                .Where(a => roomNumbers.Contains(a.RoomNumber))
                .ToListAsync();

            return apartment;
        }

        public async Task<IEnumerable<Apartment>> GetAllApartmentsAsync()
        {
            var apartment = await _context.Apartments
                .Include(a => a.Location)
                .ToListAsync();

            return apartment;
        }

        public async Task<IEnumerable<ApartmentRoom>> GetRoomsByApartmentIdAsync(int apartmentId)
        {
            var rooms = await _context.Apartments
                .Where(a => a.Id == apartmentId)
                .SelectMany(a => a.Rooms)
                .ToListAsync();

            return rooms;
        }

        public async Task<bool> IsRoomAvailable(int apartmentId, int roomId, DateTimeOffset start, DateTimeOffset end)
        {
            var calendar = await _context.Calendars
                .Where(c => c.ApartmentRoomId == roomId)
                .Where(c => start < c.End && c.Start < end)
                .FirstOrDefaultAsync();

            return calendar is null;
        }

        public async Task<IEnumerable<ApartmentRoom>> GetRoomsByApartmentIdAndDateAsync(int apartmentId, DateTimeOffset start, DateTimeOffset end)
        {
            var rooms = await _context.Apartments
                .Where(a => a.Id == apartmentId)
                .SelectMany(a => a.Rooms)
                .Where(r => r.Calendars.All(c => !(start < c.End && c.Start < end)))
                .ToListAsync();

            return rooms;
        }
    }
}
