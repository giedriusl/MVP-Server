using System.Threading.Tasks;
using MVP.Entities.Dtos.Apartments;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IApartmentService
    {
        Task<CreateApartmentDto> CreateApartment(CreateApartmentDto createApartmentDto);
        Task<UpdateApartmentDto> UpdateApartment(UpdateApartmentDto apartment);
    }
}
