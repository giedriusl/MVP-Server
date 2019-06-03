using System;
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
        Task<bool> IsRoomAvailable(int roomId, DateTimeOffset start, DateTimeOffset end);
        Task<bool> IsUserAvailable(string userId, DateTimeOffset start, DateTimeOffset end);
    }
}
