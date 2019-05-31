using MVP.Entities.Dtos.Offices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IOfficeService
    {
        Task<CreateOfficeDto> CreateOfficeAsync(CreateOfficeDto createOfficeDto);
        Task<OfficeDto> UpdateOfficeAsync(int id, OfficeDto office);
        Task DeleteOfficeAsync(int officeId);
        Task AddApartmentToOfficeId(int officeId, int apartmentId);

        Task<IEnumerable<OfficeViewDto>> GetAllOfficesAsync();
        Task<OfficeViewDto> GetOfficeByIdAsync(int officeId);
        Task<OfficeViewDto> GetOfficeByNameAsync(string officeName);
    }
}
