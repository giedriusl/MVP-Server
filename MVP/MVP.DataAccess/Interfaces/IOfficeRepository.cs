using System.Collections.Generic;
using System.Threading.Tasks;
using MVP.Entities.Entities;

namespace MVP.DataAccess.Interfaces
{
    interface IOfficeRepository
    {
        Task<Office> GetOfficeById(int officeId);
        Task<Office> GetOfficeByName(string name);
        Task<IEnumerable<Office>> GetAllOffices();

        Task AddOffice(Office office);
        Task AddOfficeList(List<Office> offices);
        Task UpdateOffice(Office office);
    }
}
