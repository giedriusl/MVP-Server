﻿using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.Offices
{
    public class OfficeDto
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
    }
}
