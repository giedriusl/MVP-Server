using System.ComponentModel.DataAnnotations;
using MVP.Entities.Entities;

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

        public static Location ToEntity(LocationCreateDto location)
        {
            return new Location
            {
                Address = location.Address,
                City = location.City,
                CountryCode = location.CountryCode
            };
        }
    }
}
