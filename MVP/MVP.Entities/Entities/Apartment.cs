using System.Collections.Generic;

namespace MVP.Entities.Entities
{
    public class Apartment
    {
        public int Id { get; private set; }
        public string Title { get; set; }
        public int? OfficeId { get; set; }
        public int LocationId { get; set; }
        public virtual Office Office { get; set; }
        public Location Location { get; set; }
        public virtual List<ApartmentRoom> Rooms { get; set; } = new List<ApartmentRoom>();

        public void UpdateApartment(string title)
        {
            Title = title;
        }
    }
}
