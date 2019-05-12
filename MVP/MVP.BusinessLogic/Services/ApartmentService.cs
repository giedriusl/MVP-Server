using MVP.BusinessLogic.Interfaces;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Dtos.Apartments;
using MVP.Entities.Exceptions;
using System;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository _apartmentRepository;

        public ApartmentService(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        public async Task<CreateApartmentDto> CreateApartment(CreateApartmentDto createApartmentDto)
        {
            try
            {
                var apartment = CreateApartmentDto.ToEntity(createApartmentDto);

                var apartmentEntity = await _apartmentRepository.AddApartment(apartment);

                return CreateApartmentDto.ToDto(apartmentEntity);
            }
            catch (Exception ex)
            {
                throw new ApartmentException(ex.Message);
            }

        }
    }
}
