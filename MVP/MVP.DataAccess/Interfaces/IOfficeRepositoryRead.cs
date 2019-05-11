using System;
using System.Collections.Generic;
using System.Text;
using MVP.Entities.Entities;

namespace MVP.DataAccess.Interfaces
{
    interface IOfficeRepositoryRead
    {
        Office GetOfficeById(int officeId);
        Office GetOfficeByName(string name);
        IEnumerable<Office> GetAllOffices();
    }
}
