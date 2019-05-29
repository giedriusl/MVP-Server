using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVP.DataAccess.Interfaces
{
    public interface ICalendarRepository
    {
        Task AddCalendarsAsync(List<Calendar> calendars);
        Task<List<Calendar>> GetCalendarByRoomAndApartmentId(int apartmentId, int roomId);
        Task<Calendar> AddAsync(Calendar calendar);
        Task DeleteAsync(Calendar calendar);
    }
}
