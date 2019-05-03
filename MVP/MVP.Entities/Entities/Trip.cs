using MVP.Entities.Enums;
using System;
using System.Collections.Generic;

namespace MVP.Entities.Entities
{
    public class Trip
    {
        public int Id { get; private set; }
        public string Title { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public int FromOfficeId { get; set; }
        public int ToOfficeId { get; set; }
        public virtual Office FromOffice { get; set; }
        public virtual Office ToOffice { get; set; }
        public TripStatus TripStatus { get; set; }
        public virtual List<FlightInformation> FlightInformations { get; set; } = new List<FlightInformation>();
        public virtual List<RentalCarInformation> RentalCarInformations { get; set; } = new List<RentalCarInformation>();
        //public virtual List<User> Users { get; private set; } = new List<User>();
    }
}
