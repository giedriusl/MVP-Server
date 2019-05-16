using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MVP.Entities.Dtos.Locations
{
    class LocationDto
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
