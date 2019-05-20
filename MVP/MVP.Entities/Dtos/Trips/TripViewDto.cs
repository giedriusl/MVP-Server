﻿using MVP.Entities.Dtos.FlightsInformation;
using MVP.Entities.Dtos.Offices;
using MVP.Entities.Dtos.RentalCarsInformation;
using MVP.Entities.Dtos.Users;
using MVP.Entities.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVP.Entities.Dtos.Trips
{
    public class TripViewDto : TripDto
    {
        [Required]
        public OfficeViewDto FromOffice { get; set; }

        [Required]
        public OfficeViewDto ToOffice { get; set; }
        public List<UserDto> Users { get; set; }


        public static TripViewDto ToDto(Trip trip)
        {
            return new TripViewDto
            {
                Id = trip.Id,
                End = trip.End,
                Start = trip.Start,
                Title = trip.Title,
                TripStatus = trip.TripStatus,
                FromOffice = OfficeViewDto.ToDto(trip.FromOffice),
                ToOffice = OfficeViewDto.ToDto(trip.ToOffice),
                FlightInformations = trip.FlightInformations.Select(FlightInformationDto.ToDto).ToList(),
                RentalCarInformations = trip.RentalCarInformations.Select(RentalCarInformationDto.ToDto).ToList(),
                Users = trip.UserTrips.Select(userTrip => userTrip.User).Select(UserDto.ToDto).ToList()
            };
        }
    }
}
