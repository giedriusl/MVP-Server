using MVP.Entities.Enums;
using System;
using System.Collections.Generic;

namespace MVP.Entities.Entities
{
    public class Trip
    {
        public Trip()
        {
            //for ef
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public DateTimeOffset Start { get; private set; }
        public DateTimeOffset End { get; private set; }
        public int FromOfficeId { get; private set; }
        public int ToOfficeId { get; private set; }
        public Office FromOffice { get; private set; }
        public Office ToOffice { get; private set; }
        public TripStatus TripStatus { get; private set; }
        public virtual List<FlightInformation> FlightInformations { get; private set; } = new List<FlightInformation>();
        public virtual List<RentalCarInformation> RentalCarInformations { get; private set; } = new List<RentalCarInformation>();
        public virtual List<User> Users { get; private set; } = new List<User>();
    }
}
