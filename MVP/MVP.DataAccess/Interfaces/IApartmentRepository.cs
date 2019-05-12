using System.Collections.Generic;
using System.Threading.Tasks;
using MVP.Entities.Entities;

namespace MVP.DataAccess.Interfaces
{
    public interface IApartmentRepository
    {
        Task AddApartment(Apartment apartment);
        Task UpdateApartment(Apartment apartment);
        Task<IEnumerable<Apartment>> GetAllApartments();
    }
}
