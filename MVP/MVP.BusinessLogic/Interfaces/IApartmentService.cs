using MVP.Entities.Dtos.Apartments;
using MVP.Entities.Dtos.Apartments.ApartmentRooms;
using MVP.Entities.Dtos.Calendars;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IApartmentService
    {
        Task<CreateApartmentDto> CreateApartment(CreateApartmentDto createApartmentDto);
        Task<UpdateApartmentDto> UpdateApartment(UpdateApartmentDto apartment);
        Task DeleteApartment(int apartmentId);

        Task<IEnumerable<ApartmentViewDto>> GetAllApartments();
        Task<ApartmentViewDto> GetApartmentById(int apartmentId);
        Task<IEnumerable<SubmitApartmentRoomDto>> GetRoomsByApartmentId(int apartmentId);
        Task<IEnumerable<CalendarDto>> GetCalendarByRoomAndApartmentId(int apartmentId, int roomId);
    }
}
