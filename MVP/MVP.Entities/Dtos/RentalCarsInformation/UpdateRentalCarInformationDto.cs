using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MVP.Entities.Dtos.RentalCarsInformation
{
    public class UpdateRentalCarInformationDto :  RentalCarInformationDto
    {
        [Required]
        public override int Id { get; set; }
    }
}
