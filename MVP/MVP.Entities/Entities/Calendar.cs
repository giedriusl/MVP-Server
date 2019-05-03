using System;

namespace MVP.Entities.Entities
{
    public class Calendar
    {
        public int Id { get; private set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        //public int UserId { get; private set; }
        //public virtual User User { get; private set; }
        public int ApartmentRoomId { get; set; }
        public virtual ApartmentRoom ApartmentRoom { get; set; }
    }
}
