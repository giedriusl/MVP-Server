using MVP.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVP.Entities.Entities
{
    public class Trip
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public string FlightTicketStatus { get; set; }
        public string RentalCarStatus { get; set; }
        [ForeignKey("FromOffice")]
        public int FromOfficeId { get; set; }
        [ForeignKey("ToOffice")]
        public int ToOfficeId { get; set; }
        public TripStatus TripStatus { get; set; }

        public Office FromOffice { get; set; }
        public Office ToOffice { get; set; }
    }
}
