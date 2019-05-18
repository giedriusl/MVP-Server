using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MVP.DataAccess.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly MvpContext _context;

        public CalendarRepository(MvpContext context)
        {
            _context = context;
        }

        public async Task AddCalendarsAsync(IEnumerable<Calendar> calendars)
        {
            _context.Calendars.AddRange(calendars);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Calendar>> GetCalendarByRoomAndApartmentId(int apartmentId, int roomId)
        {
            var calendar = await _context.Calendars
                .Where(c => c.Id == roomId)
                .ToListAsync();

            return calendar;
        }
    }
}
