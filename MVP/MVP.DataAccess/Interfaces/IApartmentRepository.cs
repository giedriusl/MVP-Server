using System.Collections.Generic;
using System.Threading.Tasks;
using MVP.Entities.Entities;

namespace MVP.DataAccess.Interfaces
{
    public interface IApartmentRepository
    {
        void AddApartment(Apartment apartment);
        void UpdateApartment(Apartment apartment);
        Task<IEnumerable<Apartment>> GetAllApartments();
    }
}
