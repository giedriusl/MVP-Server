using MVP.BusinessLogic.Interfaces;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Dtos.Locations;
using MVP.Entities.Dtos.Offices;
using MVP.Entities.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IApartmentRepository _apartmentRepository;

        public OfficeService(IOfficeRepository officeRepository,
            IApartmentRepository apartmentRepository,
            ILocationRepository locationRepository)
        {
            _officeRepository = officeRepository;
            _apartmentRepository = apartmentRepository;
            _locationRepository = locationRepository;
        }

        public async Task<CreateOfficeDto> CreateOfficeAsync(CreateOfficeDto createOfficeDto)
        {
            var office = CreateOfficeDto.ToEntity(createOfficeDto);

            var location = await _locationRepository.GetLocationByCityAndCountryCodeAndAddress
                (createOfficeDto.Location.City, createOfficeDto.Location.CountryCode, createOfficeDto.Location.Address);

            if (location is null)
            {
                location = LocationDto.ToEntity(createOfficeDto.Location);
            }

            office.Location = location;

            var entity = await _officeRepository.AddOfficeAsync(office);

            return CreateOfficeDto.ToDto(entity);
        }

        public async Task<UpdateOfficeDto> UpdateOfficeAsync(UpdateOfficeDto updateOfficeDto)
        {
            var existingOffice = await _officeRepository.GetOfficeByIdAsync(updateOfficeDto.Id);

            if (existingOffice is null)
            {
                throw new BusinessLogicException("Office was not found", "officeNotFound");
            }

            existingOffice.UpdateOffice(updateOfficeDto.Name);

            await _officeRepository.UpdateOfficeAsync(existingOffice);
            return updateOfficeDto;
        }

        public async Task DeleteOfficeAsync(int officeId)
        {
            var existingOffice = await _officeRepository.GetOfficeByIdAsync(officeId);

            if (existingOffice is null)
            {
                throw new BusinessLogicException("Office was not found", "officeNotFound");
            }

            await _officeRepository.DeleteOfficeAsync(existingOffice);
        }

        public async Task<IEnumerable<OfficeViewDto>> GetAllOfficesAsync()
        {
            var offices = await _officeRepository.GetAllOfficesAsync();
            var officesDto = offices.Select(OfficeViewDto.ToDto).ToList();

            return officesDto;
        }

        public async Task<OfficeViewDto> GetOfficeByIdAsync(int officeId)
        {
            var office = await _officeRepository.GetOfficeByIdAsync(officeId);
            return OfficeViewDto.ToDto(office);
        }

        public async Task<OfficeViewDto> GetOfficeByNameAsync(string officeName)
        {
            var office = await _officeRepository.GetOfficeByNameAsync(officeName);
            return OfficeViewDto.ToDto(office);
        }

        public async Task AddApartmentToOfficeId(int officeId, int apartmentId)
        {
            var apartment = await _apartmentRepository.GetApartmentByIdAsync(apartmentId);
            var office = await _officeRepository.GetOfficeByIdAsync(officeId);

            office.Apartments.Add(apartment);
            await _officeRepository.UpdateOfficeAsync(office);
        }
    }
}
