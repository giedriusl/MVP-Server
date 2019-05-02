using System.Collections.Generic;

namespace MVP.Entities.Entities
{
    public class Office
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public virtual List<Apartment> Apartments { get; set; } = new List<Apartment>();
    }
}
