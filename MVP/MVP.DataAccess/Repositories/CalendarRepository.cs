using System;
using Microsoft.EntityFrameworkCore;
using MVP.DataAccess.Interfaces;
using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVP.DataAccess.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly MvpContext _context;

        public CalendarRepository(MvpContext context)
        {
            _context = context;
        }

        public async Task AddCalendarsAsync(List<Calendar> calendars)
        {
            _context.Calendars.AddRange(calendars);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Calendar>> GetCalendarByRoomAndApartmentId(int apartmentId, int roomId)
        {
            var calendar = await _context.Calendars
                .Where(c => c.Id == roomId && c.ApartmentRoom.ApartmentId == apartmentId)
                .ToListAsync();

            return calendar;
        }

        public async Task<Calendar> AddAsync(Calendar calendar)
        {
            var calendarEntity = _context.Calendars.Add(calendar).Entity;
            await _context.SaveChangesAsync();

            return calendarEntity;
        }

        public async Task DeleteAsync(Calendar calendar)
        {
            _context.Calendars.Remove(calendar);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUserAvailable(string userId, DateTimeOffset start, DateTimeOffset end)
        {
            var calendar = await _context.Calendars
                .Where(c => c.UserId == userId)
                .Where(c => start < c.End && c.Start < end)
                .FirstOrDefaultAsync();

            return calendar is null;
        }

        public async Task<bool> IsRoomAvailable(int roomId, DateTimeOffset start, DateTimeOffset end)
        {
            var calendar = await _context.Calendars
                .Where(c => c.ApartmentRoomId == roomId)
                .Where(c => start < c.End && c.Start < end)
                .FirstOrDefaultAsync();

            return calendar is null;
        }
    }
}
