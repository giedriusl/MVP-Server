using System.Collections.Generic;
using System.Threading.Tasks;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;

namespace MVP.DataAccess.Repositories
{
    class OfficeRepositoryWrite : IOfficeRepositoryWrite
    {
        private readonly MvpContext _context;

        public OfficeRepositoryWrite(MvpContext context)
        {
            _context = context;
        }

        public async Task WriteOffice(Office office)
        {
            _context.Offices.Add(office);
            await _context.SaveChangesAsync();
        }

        public async Task WriteListOffice(List<Office> offices)
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
