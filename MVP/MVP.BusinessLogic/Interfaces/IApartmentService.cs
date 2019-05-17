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
        Task<UpdateApartmentDto> UpdateApartmentAsync(UpdateApartmentDto apartment);
        Task DeleteApartmentAsync(int apartmentId);

        Task<IEnumerable<ApartmentViewDto>> GetAllApartmentsAsync();
        Task<ApartmentViewDto> GetApartmentByIdAsync(int apartmentId);
        Task<IEnumerable<SubmitApartmentRoomDto>> GetRoomsByApartmentIdAsync(int apartmentId);
        Task<IEnumerable<CalendarDto>> GetCalendarByRoomAndApartmentIdAsync(int apartmentId, int roomId);
    }
}
