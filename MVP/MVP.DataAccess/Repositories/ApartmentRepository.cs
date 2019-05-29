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
        public async Task<List<ApartmentRoom>> GetRoomsByApartmentIdAsync(int apartmentId)
        {
            var room = await _context.Apartments
                .Where(a => a.Id == apartmentId)
                .SelectMany(a => a.Rooms)
                .ToListAsync();

            return room;
        }

        public async Task<IEnumerable<Apartment>> GetApartmentsByOfficeId(int officeId)
        {
            var apartments = await _context.Apartments
                .Where(apartment => apartment.OfficeId == officeId)
                .Include(apartment => apartment.Location)
                .ToListAsync();

            return apartments;
        }
    }
}
