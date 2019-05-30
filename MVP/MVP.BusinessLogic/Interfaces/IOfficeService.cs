using System.Collections.Generic;
using System.Threading.Tasks;
using MVP.Entities.Dtos.Offices;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IOfficeService
    {
        Task<CreateOfficeDto> CreateOfficeAsync(CreateOfficeDto createOfficeDto);
        Task<UpdateOfficeDto> UpdateOfficeAsync(int id, UpdateOfficeDto office);
        Task DeleteOfficeAsync(int officeId);
        Task AddApartmentToOfficeId(int officeId, int apartmentId);

        Task<IEnumerable<OfficeViewDto>> GetAllOfficesAsync();
        Task<OfficeViewDto> GetOfficeByIdAsync(int officeId);
        Task<OfficeViewDto> GetOfficeByNameAsync(string officeName);
    }
}
