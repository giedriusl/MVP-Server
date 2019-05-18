using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVP.DataAccess.Interfaces
{
    public interface ICalendarRepository
    {
        Task AddApartmentCalendar(List<Calendar> calendars);
        Task<List<Calendar>> GetCalendarByRoomAndApartmentId(int apartmentId, int roomId);
    }
}
