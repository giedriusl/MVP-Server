using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.Offices
{
    public class OfficeApartmentDto
    {
        [Required]
        public int OfficeId { get; set; }
        [Required]
        public int ApartmentId { get; set; }
    }
}
