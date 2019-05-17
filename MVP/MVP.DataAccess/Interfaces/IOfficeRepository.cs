using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVP.DataAccess.Interfaces
{
    public interface IOfficeRepository
    {
        Task<Office> AddOfficeAsync(Office office);
        Task UpdateOfficeAsync(Office office);
        Task DeleteOfficeAsync(Office office);

        Task<Office> GetOfficeByIdAsync(int officeId);
        Task<Office> GetOfficeByNameAsync(string name);
        Task<IEnumerable<Office>> GetAllOfficesAsync();
    }
}
