using System.Collections.Generic;
using System.Threading.Tasks;
using MVP.Entities.Entities;

namespace MVP.DataAccess.Interfaces
{
    interface IOfficeRepositoryRead
    {
        Task<Office> GetOfficeById(int officeId);
        Task<Office> GetOfficeByName(string name);
        Task<List<Office>> GetAllOffices();
    }
}
