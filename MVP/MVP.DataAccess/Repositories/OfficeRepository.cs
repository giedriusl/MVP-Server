using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;

namespace MVP.DataAccess.Repositories
{
    class OfficeRepository : IOfficeRepository
    {
        private readonly MvpContext _context;

        public OfficeRepository(MvpContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Office>> GetAllOffices()
        {
            var offices = await _context
                .Offices
                .Include(off => off.Location)
                .ToListAsync();

            return offices;
        }

        public async Task<Office> GetOfficeById(int officeId)
        {
            var office = await _context.Offices.Include(off => off.Location)
                .FirstOrDefaultAsync(off => off.Id == officeId);

            return office;
        }

        public async Task<Office> GetOfficeByName(string name)
        {
            var office = await _context.Offices.Include(loc => loc.Location)
                .FirstOrDefaultAsync(off => off.Name == name);

            return office;
        }

        public async Task AddOffice(Office office)
        {
            _context.Offices.Add(office);
            await _context.SaveChangesAsync();
        }

        public async Task AddOfficeList(List<Office> offices)
        {
            _context.Offices.AddRange(offices);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOffice(Office office)
        {
            _context.Offices.Update(office);
            await _context.SaveChangesAsync();
        }
    }
}
