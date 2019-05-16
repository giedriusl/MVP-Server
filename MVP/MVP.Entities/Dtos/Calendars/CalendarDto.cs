using MVP.Entities.Entities;
using System;

namespace MVP.Entities.Dtos.Calendars
{
    public class CalendarDto
    {
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }

        public static Calendar ToEntity(CalendarDto calendar)
        {
            return new Calendar
            {
                Start = calendar.Start,
                End = calendar.End
            };
        }

        public static CalendarDto ToDto(Calendar calendar)
        {
            return new CalendarDto
            {
                Start = calendar.Start,
                End = calendar.End
            };
        }
    }
}
