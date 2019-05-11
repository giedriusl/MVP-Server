using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;

namespace MVP.DataAccess.Repositories
{
    class OfficeRepository : IOfficeRepositoryRead
    {
        private readonly MvpContext context;

        public OfficeRepository(MvpContext context)
        {
            this.context = context;
        }

        public IEnumerable<Office> GetAllOffices()
        {
            IQueryable<Office> offices = context.Offices.Include(off => off.Location);

            return offices;
        }

        public Office GetOfficeById(int officeId)
        {
            Office office = context.Offices.Include(off => off.Location)
                .FirstOrDefault(off => off.Id == officeId);

            return office;
        }

        public Office GetOfficeByName(string name)
        {
            Office office = context.Offices.Include(loc => loc.Location)
                .FirstOrDefault(off => off.Name == name);

            return office;
        }
    }
}
