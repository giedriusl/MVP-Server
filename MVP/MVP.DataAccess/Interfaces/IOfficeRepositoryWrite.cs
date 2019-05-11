using System;
using System.Collections.Generic;
using System.Text;
using MVP.Entities.Entities;

namespace MVP.DataAccess.Interfaces
{
    interface IOfficeRepositoryWrite
    {
        void WriteOffice(Office office);
        void WriteListOffice(List<Office> offices);
        void UpdateOffice(Office office);
    }
}
