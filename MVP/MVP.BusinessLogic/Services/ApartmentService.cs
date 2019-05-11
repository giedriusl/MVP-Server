using MVP.BusinessLogic.Interfaces;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;

namespace MVP.BusinessLogic.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository _apartmentRepository;

        public ApartmentService(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        /*public Apartment GetApartmentById(int apartmentId)
        {

        }*/
    }
}
