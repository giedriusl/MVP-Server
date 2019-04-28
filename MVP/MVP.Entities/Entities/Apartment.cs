using System.ComponentModel.DataAnnotations.Schema;

namespace MVP.Entities.Entities
{
    public class Apartment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [ForeignKey("Office")]
        public int OfficeId { get; set; }
        public int BedCount { get; set; }
        [ForeignKey("Location")]
        public int LocationId { get; set; }

        public Office Office { get; set; }
        public Location Location { get; set; }
    }
}
