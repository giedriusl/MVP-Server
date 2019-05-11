using System;
using MVP.Entities.Entities;

namespace MVP.Entities.Dtos.Calendars
{
    public class CalendarCreateDto
    {
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }

        public static Calendar ToEntity(CalendarCreateDto calendar)
        {
            return new Calendar();
        }
    }
}
