using MVP.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MVP.Entities.Dtos.FlightsInformation;
using MVP.Entities.Dtos.RentalCarsInformation;
using MVP.Entities.Entities;

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
        public List<FlightInformationDto> FlightInformations { get; set; } = new List<FlightInformationDto>();
        public List<RentalCarInformationDto> RentalCarInformations { get; set; } = new List<RentalCarInformationDto>();

        public static TripDto ToDto(Trip trip)
        {
            return new TripDto
            {
                Id = trip.Id,
                Title = trip.Title,
                End = trip.End,
                Start = trip.Start,
                TripStatus = trip.TripStatus,
                FlightInformations = trip.FlightInformations.Select(FlightInformationDto.ToDto).ToList(),
                RentalCarInformations = trip.RentalCarInformations.Select(RentalCarInformationDto.ToDto).ToList(),
            };
        }
    }
}
