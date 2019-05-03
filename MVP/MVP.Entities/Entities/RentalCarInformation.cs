using MVP.Entities.Enums;
using System;

namespace MVP.Entities.Entities
{
    public class RentalCarInformation
    {
        public int Id { get; private set; }
        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }
        public double Cost { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public RentalCarStatus Status{ get; set; }
    }
}
