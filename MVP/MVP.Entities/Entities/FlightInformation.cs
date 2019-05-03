using MVP.Entities.Enums;
using System;

namespace MVP.Entities.Entities
{
    public class FlightInformation
    {
        public int Id { get; private set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        public double Cost { get; set; }
        public DateTimeOffset Start{ get; set; }
        public DateTimeOffset End { get; set; }
        public FlightInfomationStatus Status{ get ; set; }
    }
}
