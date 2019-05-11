using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MVP.Entities.Entities;

namespace MVP.DataAccess.Interfaces
{
    interface IOfficeRepositoryWrite
    {
        Task WriteOffice(Office office);
        Task WriteListOffice(List<Office> offices);
        Task UpdateOffice(Office office);
    }
}
