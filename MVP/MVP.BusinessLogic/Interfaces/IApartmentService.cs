using Microsoft.AspNetCore.Http;
using MVP.Entities.Dtos.Apartments;
using MVP.Entities.Dtos.Apartments.ApartmentRooms;
using MVP.Entities.Dtos.Calendars;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IApartmentService
    {
        Task<CreateApartmentDto> CreateApartmentAsync(CreateApartmentDto createApartmentDto);
        Task<UpdateApartmentDto> UpdateApartmentAsync(UpdateApartmentDto apartment);
        Task DeleteApartmentAsync(int apartmentId);
        Task AddRoomToApartmentAsync(int apartmentId, CreateApartmentRoomDto apartmentRoomDto);

        Task<IEnumerable<ApartmentViewDto>> GetAllOfficeApartmentsAsync(int officeId);
        Task<IEnumerable<ApartmentViewDto>> GetAllApartmentsAsync();
        Task<ApartmentViewDto> GetApartmentByIdAsync(int apartmentId);
        Task<IEnumerable<CreateApartmentRoomDto>> GetRoomsByApartmentIdAsync(int apartmentId);
        Task<IEnumerable<CalendarDto>> GetCalendarByRoomAndApartmentIdAsync(int apartmentId, int roomId);
        Task UploadCalendarAsync(int apartmentId, IFormFile file);
        Task UploadApartmentRoomsCalendarAsync(IFormFile file);
        Task<IEnumerable<ApartmentRoomDto>> GetAvailableRooms(int apartmentId, int tripId);
    }
}
