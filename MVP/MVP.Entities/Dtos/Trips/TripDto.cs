using MVP.Entities.Dtos.FlightsInformation;
using MVP.Entities.Dtos.Offices;
using MVP.Entities.Dtos.RentalCarsInformation;
using MVP.Entities.Dtos.Users;
using MVP.Entities.Entities;
using MVP.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        public TripStatus? TripStatus { get; set; }
        public string OrganizerId { get; set; }
        public byte[] Timestamp { get; set; }
        public List<FlightInformationDto> FlightInformations { get; set; } = new List<FlightInformationDto>();
        public List<RentalCarInformationDto> RentalCarInformations { get; set; } = new List<RentalCarInformationDto>();


        public OfficeDto FromOffice { get; set; }
        public OfficeDto ToOffice { get; set; }
        public List<UserDto> Users { get; set; } = new List<UserDto>();
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
                FromOffice = OfficeDto.ToDto(trip.FromOffice),
                ToOffice = OfficeDto.ToDto(trip.ToOffice),
                StatusName = trip.TripStatus.ToString(),
                OrganizerId = trip.OrganizerId,
                Timestamp = trip.Timestamp,
                Users = trip.UserTrips.Select(ut => UserDto.ToDto(ut.User)).ToList()
            };
        }
    }
}
