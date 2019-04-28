using System.Collections.Generic;

namespace MVP.Entities.Entities
{
    public class Office
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public List<Apartment> Apartments { get; set; } = new List<Apartment>();
    }
}
