using System.Collections.Generic;

namespace MVP.Entities.Entities
{
    public class Apartment
    {
        public Apartment()
        {
            //for ef
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public int OfficeId { get; private set; }
        public int BedCount { get; private set; }
        public int LocationId { get; private set; }
        public virtual Office Office { get; private set; }
        public Location Location { get; private set; }
        public virtual List<ApartmentRoom> Rooms { get; private set; } = new List<ApartmentRoom>();
    }
}
