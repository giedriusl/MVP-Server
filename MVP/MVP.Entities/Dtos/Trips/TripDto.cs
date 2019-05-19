using MVP.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MVP.Entities.Dtos.FlightsInformation;
using MVP.Entities.Dtos.RentalCarsInformation;

namespace MVP.Entities.Dtos.Trips
{
    public class TripDto
    {
        public virtual int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [Required]
        public DateTimeOffset Start { get; set; }

        [Required]
        public DateTimeOffset End { get; set; }

        [Required]
        public TripStatus TripStatus { get; set; }

        public List<FlightInformationDto> FlightsInformation { get; set; } = new List<FlightInformationDto>();
        public List<RentalCarInformationDto> RentalCarsInformation { get; set; } = new List<RentalCarInformationDto>();

    }
}
