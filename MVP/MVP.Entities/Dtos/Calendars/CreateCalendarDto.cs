using System;
using MVP.Entities.Entities;

namespace MVP.Entities.Dtos.Calendars
{
    public class CreateCalendarDto
    {
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }

        public static Calendar ToEntity(CreateCalendarDto createCalendar)
        {
            return new Calendar
            {
                Start = createCalendar.Start,
                End = createCalendar.End
            };
        }
    }
}
