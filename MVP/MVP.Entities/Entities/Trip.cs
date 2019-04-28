using MVP.Entities.Enums;
using System;

namespace MVP.Entities.Entities
{
    public class Trip
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public string FlightTicketStatus { get; set; } //todo: other table
        public string RentalCarStatus { get; set; }
        public int FromOfficeId { get; set; }
        public int ToOfficeId { get; set; }
        public Office FromOffice { get; set; }
        public Office ToOffice { get; set; }
        public TripStatus TripStatus { get; set; }
    }
}
