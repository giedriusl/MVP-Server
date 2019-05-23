using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MVP.Entities.Dtos.FlightsInformation;
using MVP.Entities.Dtos.RentalCarsInformation;
using MVP.Entities.Dtos.Users;
using MVP.Entities.Entities;

namespace MVP.Entities.Dtos.Trips
{
    public class UpdateTripDto : CreateTripDto
    {
        [Required]
        public override int Id { get; set; }
    }
}
