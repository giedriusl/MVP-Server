using System;
using System.Collections.Generic;
using System.Text;
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

        public void WriteOffice(Office office)
        {
            _context.Offices.Add(office);
            _context.SaveChanges();
        }

        public void WriteListOffice(List<Office> offices)
        {
            _context.Offices.AddRange(offices);
            _context.SaveChanges();
        }

        public void UpdateOffice(Office office)
        {
            _context.Offices.Update(office);
            _context.SaveChanges();
        }
    }
}
