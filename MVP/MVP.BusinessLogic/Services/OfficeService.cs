using MVP.BusinessLogic.Interfaces;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Dtos.Offices;
using MVP.Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVP.Entities.Dtos.Locations;

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
            try
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
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        public async Task<UpdateOfficeDto> UpdateOfficeAsync(UpdateOfficeDto updateOfficeDto)
        {
            try
            {
                var existingOffice = await _officeRepository.GetOfficeByIdAsync(updateOfficeDto.Id);

                if (existingOffice is null)
                {
                    throw new BusinessLogicException("Office was not found");
                }

                existingOffice.UpdateOffice(updateOfficeDto.Name);

                await _officeRepository.UpdateOfficeAsync(existingOffice);
                return updateOfficeDto;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex, "Failed to update office");
            }
        }

        public async Task DeleteOfficeAsync(int officeId)
        {
            try
            {
                var existingOffice = await _officeRepository.GetOfficeByIdAsync(officeId);

                if (existingOffice is null)
                {
                    throw new BusinessLogicException("Office was not found");
                }

                await _officeRepository.DeleteOfficeAsync(existingOffice);
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex, "Failed to update office");
            }
        }

        public async Task<IEnumerable<OfficeViewDto>> GetAllOfficesAsync()
        {
            try
            {
                var offices = await _officeRepository.GetAllOfficesAsync();

                var officesDto = offices.Select(OfficeViewDto.ToDto).ToList();

                return officesDto;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex, "Failed to get all offices");
            }
        }

        public async Task<OfficeViewDto> GetOfficeByIdAsync(int officeId)
        {
            try
            {
                var office = await _officeRepository.GetOfficeByIdAsync(officeId);

                return OfficeViewDto.ToDto(office);
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex, $"Failed to get office {officeId}");
            }
        }

        public async Task<OfficeViewDto> GetOfficeByNameAsync(string officeName)
        {
            try
            {
                var office = await _officeRepository.GetOfficeByNameAsync(officeName);

                return OfficeViewDto.ToDto(office);
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex, $"Failed to get office {officeName}");
            }
        }

        public async Task AddApartmentToOfficeId(int officeId, int apartmentId)
        {
            try
            {
                var apartment = await _apartmentRepository.GetApartmentByIdAsync(apartmentId);
                var office = await _officeRepository.GetOfficeByIdAsync(officeId);

                office.Apartments.Add(apartment);

                await _officeRepository.UpdateOfficeAsync(office);
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex, $"Failed to add apartment {apartmentId} to office {officeId}");
            }
        }
    }
}
