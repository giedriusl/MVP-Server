using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVP.DataAccess.Interfaces
{
    public interface ICalendarRepository
    {
        Task AddCalendarListAsync(List<Calendar> calendars);
        Task<List<Calendar>> GetCalendarByRoomAndApartmentId(int apartmentId, int roomId);
    }
}
