using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.Apartments
{
    public class ApartmentDto
    {
        [Required]
        [StringLength(256)]
        public string Title { get; set; }
        public int? OfficeId { get; set; }

        [Required]
        public int BedCount { get; set; }

        //public Office Office { get; set; }
    }
}
