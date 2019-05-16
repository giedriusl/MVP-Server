using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MVP.Entities.Dtos.Locations;

namespace MVP.Entities.Dtos.Offices
{
    class OfficeDto
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        public LocationDto LocationDto { get; set; }
    }
}
