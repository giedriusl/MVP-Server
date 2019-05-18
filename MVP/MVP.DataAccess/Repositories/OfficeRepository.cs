using Microsoft.EntityFrameworkCore;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVP.DataAccess.Repositories
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly MvpContext _context;

        public OfficeRepository(MvpContext context)
        {
            _context = context;
        }
        public async Task<Office> AddOfficeAsync(Office office)
        {
            var officeEntity = _context.Offices.Add(office).Entity;
            await _context.SaveChangesAsync();

            return officeEntity;
        }
        public async Task UpdateOfficeAsync(Office office)
        {
            _context.Offices.Update(office);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOfficeAsync(Office office)
        {
            _context.Offices.Remove(office);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Office>> GetAllOfficesAsync()
        {
            var offices = await _context
                .Offices
                .Include(off => off.Location)
                .ToListAsync();

            return offices;
        }

        public async Task<Office> GetOfficeByIdAsync(int officeId)
        {
            var office = await _context.Offices.Include(off => off.Location)
                .FirstOrDefaultAsync(off => off.Id == officeId);

            return office;
        }

        public async Task<Office> GetOfficeByNameAsync(string name)
        {
            var office = await _context.Offices.Include(loc => loc.Location)
                .FirstOrDefaultAsync(off => off.Name == name);

            return office;
        }
    }
}
