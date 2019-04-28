using System;

namespace MVP.Entities.Entities
{
    public class FlightInformation
    {
        public int Id { get; private set; }
        public int TripId { get; private set; }
        public Trip Trip { get; private set; }
        public double Cost { get; private set; }
        public DateTimeOffset Start{ get; private set; }
        public DateTimeOffset End { get; private set; }
        public int FlightInformationStatusId { get; private set; }
        //public FlightInfomationStatus Status{ get => ; private set; }
    }
}
