using MVP.Entities.Dtos.FlightsInformation;
using MVP.Entities.Dtos.RentalCarsInformation;
using MVP.Entities.Entities;
using MVP.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
        public string OrganizerId { get; set; }
        public byte[] Timestamp { get; set; }
        public List<FlightInformationDto> FlightInformations { get; set; } = new List<FlightInformationDto>();
        public List<RentalCarInformationDto> RentalCarInformations { get; set; } = new List<RentalCarInformationDto>();


        public string FromOfficeName { get; set; }
        public string ToOfficeName { get; set; }
        public string StatusName { get; set; }

        public static TripDto ToDto(Trip trip)
        {
            return new TripDto
            {
                Id = trip.Id,
                Title = trip.Title,
                Start = trip.Start,
                End = trip.End,
                TripStatus = trip.TripStatus,
                FromOfficeName = trip.FromOffice.Name,
                ToOfficeName = trip.ToOffice.Name,
                StatusName = trip.TripStatus.ToString(),
                OrganizerId = trip.OrganizerId,
                Timestamp = trip.Timestamp
            };
        }
    }
}
