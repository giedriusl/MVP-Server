using System.Collections.Generic;
using MVP.Entities.Dtos.FlightsInformation;
using MVP.Entities.Dtos.RentalCarsInformation;
using MVP.Entities.Entities;
using System.Linq;
using MVP.Entities.Dtos.Users;

namespace MVP.Entities.Dtos.Trips
{
    public class MergedTripDto : TripDto
    {
        public List<UserDto> Users { get; set; } = new List<UserDto>();

        public static MergedTripDto ToDto(Trip trip)
        {
            var mergedTrip = new MergedTripDto
            {
                Id = trip.Id,
                Title = trip.Title,
                End = trip.End,
                Start = trip.Start,
                TripStatus = trip.TripStatus,
                FlightInformations = trip.FlightInformations.Select(FlightInformationDto.ToDto).ToList(),
                RentalCarInformations = trip.RentalCarInformations.Select(RentalCarInformationDto.ToDto).ToList(),
                Users = trip.UserTrips.Select(userTrip => UserDto.ToDto(userTrip.User)).ToList()
            };

            return mergedTrip;
        }
    }
}
