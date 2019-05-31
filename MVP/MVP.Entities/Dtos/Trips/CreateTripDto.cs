using MVP.Entities.Dtos.FlightsInformation;
using MVP.Entities.Dtos.RentalCarsInformation;
using MVP.Entities.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

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
                FlightInformations = createTripDto.FlightInformations.Select(FlightInformationDto.ToEntity).ToList(),
                RentalCarInformations = createTripDto.RentalCarInformations.Select(RentalCarInformationDto.ToEntity).ToList(),
                OrganizerId = createTripDto.OrganizerId
            };
        }

        public new static CreateTripDto ToDto(Trip trip)
        {
            return new CreateTripDto
            {
                Id = trip.Id,
                Title = trip.Title,
                End = trip.End,
                Start = trip.Start,
                TripStatus = trip.TripStatus,
                FlightInformations = trip.FlightInformations.Select(FlightInformationDto.ToDto).ToList(),
                RentalCarInformations = trip.RentalCarInformations.Select(RentalCarInformationDto.ToDto).ToList(),
                FromOfficeId = trip.FromOfficeId,
                ToOfficeId = trip.ToOfficeId,
                UserIds = trip.UserTrips.Select(userTrip => userTrip.UserId).ToList(),
                OrganizerId = trip.OrganizerId,
                Timestamp = trip.Timestamp
            };
        }
    }
}
