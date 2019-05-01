using System;

namespace MVP.Entities.Entities
{
    public class Calendar
    {
        public Calendar()
        {
            //for ef
        }

        public int Id { get; private set; }
        public DateTimeOffset Start { get; private set; }
        public DateTimeOffset End { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }
        public int ApartmentId { get; private set; }
        public Apartment Apartment { get; private set; }
    }
}
