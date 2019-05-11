using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.Locations
{
    public class LocationCreateDto
    {
        [Required]
        [StringLength(256)]
        public string City { get; set; }

        [Required]
        [StringLength(256)]
        public string CountryCode { get; set; }

        [Required]
        [StringLength(256)]
        public string Address { get; set; }
    }
}
