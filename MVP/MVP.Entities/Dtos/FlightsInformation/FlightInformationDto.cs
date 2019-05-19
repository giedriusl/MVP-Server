using System;
using System.ComponentModel.DataAnnotations;
using MVP.Entities.Entities;
using MVP.Entities.Enums;

namespace MVP.Entities.Dtos.FlightsInformation
{
    public class FlightInformationDto
    {
        public virtual int Id { get; set; }

        [Required]
        public int TripId { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public DateTimeOffset Start { get; set; }

        [Required]
        public DateTimeOffset End { get; set; }

        [Required]
        public FlightInfomationStatus Status { get; set; }


        public static FlightInformationDto ToDto(FlightInformation flightInformation)
        {
            return new FlightInformationDto
            {
                Id = flightInformation.Id,
                Cost = flightInformation.Cost,
                End = flightInformation.End,
                Start = flightInformation.Start,
                Status = flightInformation.Status,
                TripId = flightInformation.TripId
            };
        }

        public static FlightInformation ToEntity(FlightInformationDto createFlightInformationDto)
        {
            return new FlightInformation
            {
                Cost = createFlightInformationDto.Cost,
                End = createFlightInformationDto.End,
                Start = createFlightInformationDto.Start,
                Status = createFlightInformationDto.Status,
                TripId = createFlightInformationDto.TripId
            };
        }
    }
}
