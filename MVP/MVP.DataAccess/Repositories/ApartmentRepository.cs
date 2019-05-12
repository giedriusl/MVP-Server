using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;

namespace MVP.DataAccess.Repositories
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly MvpContext _context;

        public ApartmentRepository(MvpContext context)
        {
            _context = context;
        }

        public async Task AddApartment(Apartment apartment)
        {
            _context.Apartments.Add(apartment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateApartment(Apartment apartment)
        {
            _context.Apartments.Update(apartment);
            await _context.SaveChangesAsync();
        }
        public async Task<Apartment> GetApartmentById(int apartmentId)
        {
            var apartment = await _context.Apartments
                .FirstOrDefaultAsync(x => x.Id == apartmentId);

            return apartment;
        }
        public async Task<IEnumerable<Apartment>> GetAllApartments()
        {
            var apartment = await _context.Apartments
                .ToListAsync();

            return apartment;
        }
    }
}
