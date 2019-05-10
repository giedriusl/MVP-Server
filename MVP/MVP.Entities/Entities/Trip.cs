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
        public virtual ICollection<FlightInformation> FlightInformations { get; set; }
        public virtual ICollection<RentalCarInformation> RentalCarInformations { get; set; }
        public ICollection<UserTrip> UserTrips { get; set; }
    }
}
