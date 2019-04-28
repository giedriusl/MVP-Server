using System.ComponentModel.DataAnnotations.Schema;

namespace MVP.Entities.Entities
{
    public class Office
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Location")]
        public int LocationId { get; set; }

        public Location Location { get; set; }
    }
}
