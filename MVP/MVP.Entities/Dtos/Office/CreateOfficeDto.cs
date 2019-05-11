using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MVP.Entities.Entities;

namespace MVP.Entities.Dtos.Office
{
    class CreateOfficeDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        public LocationCreateDto LocationCreateDto { get; set; }

        public CreateOfficeDto(int officeId, string name, LocationCreateDto locationCreateDto)
        {
            Id = officeId;
            Name = name;
            LocationCreateDto = locationCreateDto;
        }

    }
}
