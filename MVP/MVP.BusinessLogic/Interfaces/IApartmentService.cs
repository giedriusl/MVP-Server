using System.Threading.Tasks;
using MVP.Entities.Dtos.Apartments;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IApartmentService
    {
        Task CreateApartment(CreateApartmentDto createApartmentDto);
    }
}
