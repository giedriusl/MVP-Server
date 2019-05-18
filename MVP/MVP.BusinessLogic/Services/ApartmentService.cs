using MVP.BusinessLogic.Interfaces;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Dtos.Apartments;
using MVP.Entities.Dtos.Apartments.ApartmentRooms;
using MVP.Entities.Dtos.Calendars;
using MVP.Entities.Dtos.Locations;
using MVP.Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly ICalendarRepository _calendarRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IOfficeRepository _officeRepository;

        public ApartmentService(IApartmentRepository apartmentRepository,
            ICalendarRepository calendarRepository,
            ILocationRepository locationRepository, 
            IOfficeRepository officeRepository)
        {
            _apartmentRepository = apartmentRepository;
            _calendarRepository = calendarRepository;
            _locationRepository = locationRepository;
            _officeRepository = officeRepository;
        }

        public async Task<CreateApartmentDto> CreateApartmentAsync(CreateApartmentDto createApartmentDto)
        {
            try
            {
                var apartment = CreateApartmentDto.ToEntity(createApartmentDto);

                if (createApartmentDto.OfficeId != 0)
                {
                    var office = await _officeRepository.GetOfficeByIdAsync(createApartmentDto.OfficeId);
                    apartment.Office = office ?? throw new BusinessLogicException("Office not found");
                }

                var location = await _locationRepository.GetLocationByCityAndCountryCodeAndAddress
                    (createApartmentDto.Location.City, createApartmentDto.Location.CountryCode, createApartmentDto.Location.Address);

                if (location is null)
                {
                    location = LocationDto.ToEntity(createApartmentDto.Location);
                }

                apartment.Location = location;

                var apartmentEntity = await _apartmentRepository.AddApartmentAsync(apartment);

                return CreateApartmentDto.ToDto(apartmentEntity);
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }
        public async Task<UpdateApartmentDto> UpdateApartmentAsync(UpdateApartmentDto updateApartmentDto)
        {
            try
            {
                var apartment = await _apartmentRepository.GetApartmentByIdAsync(updateApartmentDto.Id);

                if (apartment is null)
                {
                    throw new BusinessLogicException("Apartment was not found");
                }

                apartment.UpdateApartment(updateApartmentDto.Title, updateApartmentDto.BedCount);

                await _apartmentRepository.UpdateApartmentAsync(apartment);
                return updateApartmentDto;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex, "Failed to update apartment");
            }
        }
        public async Task DeleteApartmentAsync(int apartmentId)
        {
            try
            {
                var existingApartment = await _apartmentRepository.GetApartmentByIdAsync(apartmentId);

                if (existingApartment is null)
                {
                    throw new BusinessLogicException("Apartment was not found");
                }

                await _apartmentRepository.DeleteApartmentAsync(existingApartment);
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex, "Failed to update apartment");
            }
        }

        public async Task<IEnumerable<ApartmentViewDto>> GetAllApartmentsAsync()
        {
            try
            {
                var apartments = await _apartmentRepository.GetAllApartmentsAsync();

                var apartmentsDto = apartments.Select(ApartmentViewDto.ToDto).ToList();

                return apartmentsDto;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex, "Failed to get all apartments");
            }
        }

        public async Task<ApartmentViewDto> GetApartmentByIdAsync(int apartmentId)
        {
            try
            {
                var apartment = await _apartmentRepository.GetApartmentByIdAsync(apartmentId);

                return ApartmentViewDto.ToDto(apartment);
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex, $"Failed to get apartment {apartmentId}");
            }
        }

        public async Task<IEnumerable<CreateApartmentRoomDto>> GetRoomsByApartmentIdAsync(int apartmentId)
        {
            try
            {
                var rooms = await _apartmentRepository.GetRoomsByApartmentIdAsync(apartmentId);
                return rooms.Select(CreateApartmentRoomDto.ToDto).ToList();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex, $"Failed to get all rooms {apartmentId}");
            }
        }

        public async Task<IEnumerable<CalendarDto>> GetCalendarByRoomAndApartmentIdAsync(int apartmentId, int roomId)
        {
            try
            {
                var calendars = await _calendarRepository.GetCalendarByRoomAndApartmentId(apartmentId, roomId);

                return calendars.Select(CalendarDto.ToDto).ToList();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex, $"Failed to get {apartmentId} room calendar. ");
            }
        }
    }
}
