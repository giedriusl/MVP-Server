using MVP.BusinessLogic.Interfaces;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Dtos.Apartments;
using MVP.Entities.Dtos.Apartments.ApartmentRooms;
using MVP.Entities.Dtos.Calendars;
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

        public ApartmentService(IApartmentRepository apartmentRepository, ICalendarRepository calendarRepository)
        {
            _apartmentRepository = apartmentRepository;
            _calendarRepository = calendarRepository;
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
        public async Task<UpdateApartmentDto> UpdateApartment(UpdateApartmentDto apartment)
        {
            try
            {
                var existingApartment = await _apartmentRepository.GetApartmentWithRoomsById(apartment.Id);

                if (existingApartment is null)
                {
                    throw new ApartmentException("Apartment was not found");
                }

                existingApartment.UpdateApartment(apartment.Title, apartment.BedCount);

                await _apartmentRepository.UpdateApartment(existingApartment);
                return apartment;
            }
            catch (Exception ex)
            {
                throw new ApartmentException(ex, "Failed to update apartment");
            }
        }
        public async Task DeleteApartment(int apartmentId)
        {
            try
            {
                var existingApartment = await _apartmentRepository.GetApartmentWithRoomsById(apartmentId);

                if (existingApartment is null)
                {
                    throw new ApartmentException("Apartment was not found");
                }

                await _apartmentRepository.DeleteApartment(existingApartment);
            }
            catch (Exception ex)
            {
                throw new ApartmentException(ex, "Failed to update apartment");
            }
        }

        public async Task<IEnumerable<ApartmentViewDto>> GetAllApartments()
        {
            try
            {
                var apartments = await _apartmentRepository.GetAllApartments();

                var apartmentsDto = apartments.Select(ApartmentViewDto.ToDto).ToList();

                return apartmentsDto;
            }
            catch (Exception ex)
            {
                throw new ApartmentException(ex, "Failed to get all apartments");
            }
        }

        public async Task<ApartmentViewDto> GetApartmentById(int apartmentId)
        {
            try
            {
                var apartment = await _apartmentRepository.GetApartmentById(apartmentId);

                return ApartmentViewDto.ToDto(apartment);
            }
            catch (Exception ex)
            {
                throw new ApartmentException(ex, $"Failed to get apartment {apartmentId}");
            }
        }

        public async Task<IEnumerable<SubmitApartmentRoomDto>> GetRoomsByApartmentId(int apartmentId)
        {
            try
            {
                var rooms = await _apartmentRepository.GetRoomsByApartmentId(apartmentId);
                return rooms.Select(SubmitApartmentRoomDto.ToDto).ToList();
            }
            catch (Exception ex)
            {
                throw new ApartmentException(ex, $"Failed to get all rooms {apartmentId}");
            }
        }

        public async Task<IEnumerable<CalendarDto>> GetCalendarByRoomAndApartmentId(int apartmentId, int roomId)
        {
            try
            {
                var calendars = await _calendarRepository.GetCalendarByRoomAndApartmentId(apartmentId, roomId);

                return calendars.Select(CalendarDto.ToDto).ToList();
            }
            catch (Exception ex)
            {
                throw new ApartmentException(ex, $"Failed to get {apartmentId} room calendar. ");
            }
        }
    }
}
