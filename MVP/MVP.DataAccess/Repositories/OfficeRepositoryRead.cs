using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;

namespace MVP.DataAccess.Repositories
{
    class OfficeRepository : IOfficeRepositoryRead
    {
        private readonly MvpContext _context;

        public OfficeRepository(MvpContext context)
        {
            _context = context;
        }

        public async Task<List<Office>> GetAllOffices()
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
    }
}
