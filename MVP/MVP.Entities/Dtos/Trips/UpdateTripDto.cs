using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using MVP.Entities.Dtos.FlightsInformation;
using MVP.Entities.Dtos.RentalCarsInformation;
using MVP.Entities.Entities;

namespace MVP.Entities.Dtos.Trips
{
    public class UpdateTripDto : TripDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int FromOfficeId { get; set; }

        [Required]
        public int ToOfficeId { get; set; }
    }
}
