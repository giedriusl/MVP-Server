using System;
using System.Collections.Generic;

namespace MVP.Entities.Entities
{
    public class Calendar
    {
        public int Id { get; private set; }
        public DateTimeOffset Start { get; private set; }
        public DateTimeOffset End { get; private set; }
        //public int UserId { get; private set; }
        //public virtual User User { get; private set; }
        public int ApartmentRoomId { get; private set; }
        public virtual ApartmentRoom Apartment { get; private set; }
    }
}
