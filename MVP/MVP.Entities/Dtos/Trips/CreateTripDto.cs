using MVP.Entities.Dtos.FlightsInformation;
using MVP.Entities.Dtos.Offices;
using MVP.Entities.Dtos.RentalCarsInformation;
using MVP.Entities.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVP.Entities.Dtos.Trips
{
    public class CreateTripDto : TripDto
    {
        [Required]
        public int FromOfficeId;

        [Required]
        public int ToOfficeId;
        public List<string> UserIds { get; set; }

        public static Trip ToEntity(CreateTripDto createTripDto)
        {
            return new Trip
            {
                Title = createTripDto.Title,
                End = createTripDto.End,
                Start = createTripDto.Start,
                TripStatus = createTripDto.TripStatus,
                FlightInformations = createTripDto.FlightsInformation.Select(FlightInformationDto.ToEntity).ToList(),
                RentalCarInformations = createTripDto.RentalCarsInformation.Select(RentalCarInformationDto.ToEntity).ToList()
            };
        }

        public static CreateTripDto ToDto(Trip trip)
        {
            return new CreateTripDto
            {
                Id = trip.Id,
                Title = trip.Title,
                End = trip.End,
                Start = trip.Start,
                TripStatus = trip.TripStatus,
                FlightsInformation = trip.FlightInformations.Select(FlightInformationDto.ToDto).ToList(),
                RentalCarsInformation = trip.RentalCarInformations.Select(RentalCarInformationDto.ToDto).ToList(),
                FromOfficeId = trip.FromOfficeId,
                ToOfficeId = trip.ToOfficeId,
                UserIds = trip.UserTrips.Select(userTrip => userTrip.UserId).ToList()
            };
        }
    }
}
