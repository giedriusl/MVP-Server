using MVP.Entities.Dtos.FlightsInformation;
using MVP.Entities.Enums;
using System;

namespace MVP.Entities.Entities
{
    public class FlightInformation
    {
        public int Id { get; private set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        public double Cost { get; set; }
        public DateTimeOffset Start{ get; set; }
        public DateTimeOffset End { get; set; }
        public FlightInformationStatus Status{ get ; set; }
        public string FromAirport { get; set; }
        public string ToAirport { get; set; }

        public void UpdateFlightInformation(UpdateFlightInformationDto updateFlightInformationDto)
        {
            Cost = updateFlightInformationDto.Cost;
            Start = updateFlightInformationDto.Start;
            End = updateFlightInformationDto.End;
            Status = updateFlightInformationDto.Status;
            FromAirport = updateFlightInformationDto.FromAirport;
            ToAirport = updateFlightInformationDto.ToAirport;
        }
    }
}
