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

        public void AddApartment(Apartment apartment)
        {
            _context.Apartments.Add(apartment);
            _context.SaveChanges();
        }

        public void UpdateApartment(Apartment apartment)
        {
            _context.Apartments.Update(apartment);
            _context.SaveChanges();
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
