using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MVP.Entities.Dtos.FlightsInformation;
using MVP.Entities.Dtos.RentalCarsInformation;
using MVP.Entities.Dtos.Users;
using MVP.Entities.Entities;

namespace MVP.Entities.Dtos.Trips
{
    public class UpdateTripDto : TripDto
    {
        [Required]
        public override int Id { get; set; }

        [Required]
        public int FromOfficeId { get; set; }

        [Required]
        public int ToOfficeId { get; set; }
        public List<UserDto> Users { get; set; }


        public new static UpdateTripDto ToDto(Trip trip)
        {
            return new UpdateTripDto
            {
                End = trip.End,
                Start = trip.Start,
                Title = trip.Title,
                FromOfficeId = trip.FromOfficeId,
                ToOfficeId = trip.ToOfficeId,
                TripStatus = trip.TripStatus,
                FlightInformations = trip.FlightInformations.Select(FlightInformationDto.ToDto).ToList(),
                RentalCarInformations = trip.RentalCarInformations.Select(RentalCarInformationDto.ToDto).ToList(),
                Users = trip.UserTrips.Select(userTrip => userTrip.User).Select(UserDto.ToDto).ToList()
            };
        }
    }
}
