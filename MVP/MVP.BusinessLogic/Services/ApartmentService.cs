using System;
using Microsoft.AspNetCore.Http;
using MVP.BusinessLogic.Interfaces;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Dtos.Apartments;
using MVP.Entities.Dtos.Apartments.ApartmentRooms;
using MVP.Entities.Dtos.Calendars;
using MVP.Entities.Dtos.Locations;
using MVP.Entities.Exceptions;
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
        private readonly IFileReader _fileReader;
        private readonly ITripRepository _tripRepository;
        private readonly ITripApartmentInfoRepository _tripApartmentInfoRepository;

        public ApartmentService(IApartmentRepository apartmentRepository,
            ICalendarRepository calendarRepository,
            ILocationRepository locationRepository,
            IOfficeRepository officeRepository,
            IFileReader fileReader, 
            ITripRepository tripRepository, 
            ITripApartmentInfoRepository tripApartmentInfoRepository)
        {
            _apartmentRepository = apartmentRepository;
            _calendarRepository = calendarRepository;
            _locationRepository = locationRepository;
            _officeRepository = officeRepository;
            _fileReader = fileReader;
            _tripRepository = tripRepository;
            _tripApartmentInfoRepository = tripApartmentInfoRepository;
        }

        public async Task<CreateApartmentDto> CreateApartmentAsync(CreateApartmentDto createApartmentDto)
        {
            var apartment = CreateApartmentDto.ToEntity(createApartmentDto);

            if (createApartmentDto.OfficeId != null)
            {
                var office = await _officeRepository.GetOfficeByIdAsync(createApartmentDto.OfficeId.Value);
                apartment.Office = office ?? throw new BusinessLogicException("Office  does not exist", "officeNotFound");
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

        public async Task AddRoomToApartmentAsync(int apartmentId, CreateApartmentRoomDto apartmentRoomDto)
        {
            var apartment = await _apartmentRepository.GetApartmentWithRoomsByIdAsync(apartmentId);

            if (apartment is null)
            {
                throw new BusinessLogicException("Apartment does not exist", "apartmentNotFound");
            }

            var apartmentRoom = CreateApartmentRoomDto.ToEntity(apartmentRoomDto);

            apartment.Rooms.Add(apartmentRoom);

            await _apartmentRepository.UpdateApartmentAsync(apartment);
        }

        public async Task<ApartmentDto> UpdateApartmentAsync(int apartmentId, ApartmentDto updateApartmentDto)
        {
            var apartment = await _apartmentRepository.GetApartmentByIdAsync(apartmentId);

            if (apartment is null)
            {
                throw new BusinessLogicException("Apartment was not found", "apartmentNotFound");
            }

            apartment.UpdateApartment(updateApartmentDto.Title);

            await _apartmentRepository.UpdateApartmentAsync(apartment);
            return updateApartmentDto;
        }
        public async Task DeleteApartmentAsync(int apartmentId)
        {
            var existingApartment = await _apartmentRepository.GetApartmentByIdAsync(apartmentId);

            if (existingApartment is null)
            {
                throw new BusinessLogicException("Apartment was not found", "apartmentNotFound");
            }

            await _apartmentRepository.DeleteApartmentAsync(existingApartment);
        }

        public async Task<IEnumerable<ApartmentViewDto>> GetAllApartmentsAsync()
        {
            var apartments = await _apartmentRepository.GetAllApartmentsAsync();
            var apartmentsDto = apartments.Select(ApartmentViewDto.ToDto).ToList();

            return apartmentsDto;
        }

        public async Task<ApartmentViewDto> GetApartmentByIdAsync(int apartmentId)
        {
            var apartment = await _apartmentRepository.GetApartmentByIdAsync(apartmentId);
            return ApartmentViewDto.ToDto(apartment);
        }

        public async Task<IEnumerable<CreateApartmentRoomDto>> GetRoomsByApartmentIdAsync(int apartmentId)
        {
            var rooms = await _apartmentRepository.GetRoomsByApartmentIdAsync(apartmentId);
            return rooms.Select(CreateApartmentRoomDto.ToDto).ToList();
        }

        public async Task<IEnumerable<CalendarDto>> GetCalendarByRoomAndApartmentIdAsync(int apartmentId, int roomId)
        {
            var calendars = await _calendarRepository.GetCalendarByRoomAndApartmentId(apartmentId, roomId);
            return calendars.Select(CalendarDto.ToDto).ToList();
        }

        public async Task UploadCalendarAsync(int apartmentId, IFormFile file)
        {
            var calendars = await _fileReader.ReadApartmentCalendarFileAsync(apartmentId, file);
            await _calendarRepository.AddCalendarsAsync(calendars.ToList());
        }

        public async Task UploadApartmentRoomsCalendarAsync(IFormFile file)
        {
            var calendars = await _fileReader.ReadApartmentRoomCalendarFileAsync(file);
            await _calendarRepository.AddCalendarsAsync(calendars.ToList());
        }

        public async Task<IEnumerable<ApartmentViewDto>> GetAllOfficeApartmentsAsync(int officeId)
        {
            var office = await _officeRepository.GetOfficeByIdAsync(officeId);

            if (office is null)
            {
                throw new BusinessLogicException("Specified office does not exist", "officeNotFound");
            }

            var apartments = await _apartmentRepository.GetApartmentsByOfficeId(officeId);
            var apartmentsViewDto = apartments.Select(ApartmentViewDto.ToDto);

            return apartmentsViewDto;
        }

        public async Task<IEnumerable<ApartmentRoomDto>> GetAvailableRooms(int apartmentId, int tripId)
        {
            var trip = await _tripRepository.GetTripByIdAsync(tripId);
            if (trip is null)
            {
                throw new BusinessLogicException("Trip was not found", "tripNotFound");
            }

            var rooms = await _apartmentRepository.GetRoomsByApartmentIdAndDateAsync(apartmentId, trip.Start, trip.End);
            return rooms.Select(ApartmentRoomDto.ToDto);
        }
    }
}
