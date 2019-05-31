using Microsoft.AspNetCore.Http;
using MVP.Entities.Dtos.Apartments;
using MVP.Entities.Dtos.Apartments.ApartmentRooms;
using MVP.Entities.Dtos.Calendars;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IApartmentService
    {
        Task<CreateApartmentDto> CreateApartmentAsync(CreateApartmentDto createApartmentDto);
        Task<ApartmentDto> UpdateApartmentAsync(int apartmentId, ApartmentDto apartment);
        Task DeleteApartmentAsync(int apartmentId);
        Task AddRoomToApartmentAsync(int apartmentId, CreateApartmentRoomDto apartmentRoomDto);

        Task<IEnumerable<ApartmentViewDto>> GetAllOfficeApartmentsAsync(int officeId);
        Task<IEnumerable<ApartmentViewDto>> GetAllApartmentsAsync();
        Task<ApartmentViewDto> GetApartmentByIdAsync(int apartmentId);
        Task<IEnumerable<CreateApartmentRoomDto>> GetRoomsByApartmentIdAsync(int apartmentId);
        Task<IEnumerable<CalendarDto>> GetCalendarByRoomAndApartmentIdAsync(int apartmentId, int roomId);
        Task UploadApartmentRoomsCalendarAsync(IFormFile file);
        Task<IEnumerable<ApartmentRoomDto>> GetAvailableRooms(int apartmentId, int tripId);
        Task DeleteRoomAsync(int apartmentRoomId);
    }
}
