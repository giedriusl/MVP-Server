using MVP.Entities.Dtos.FlightsInformation;
using MVP.Entities.Dtos.RentalCarsInformation;
using MVP.Entities.Dtos.Users;
using MVP.Entities.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MVP.Entities.Dtos.Trips
{
    public class MergedTripDto : TripDto
    {
        public int BaseTripId { get; set; }
        public int AdditionalTripId { get; set; }

        public List<UserDto> Users { get; set; } = new List<UserDto>();

        public new static MergedTripDto ToDto(Trip trip)
        {
            var mergedTrip = new MergedTripDto
            {
                BaseTripId = trip.Id,
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

        public static CreateTripDto ToCreateTripDto(MergedTripDto mergedTripDto, int toOfficeId, int fromOfficeId, string organizerId)
        {
            return new CreateTripDto
            {
                Title = mergedTripDto.Title,
                End = mergedTripDto.End,
                Start = mergedTripDto.Start,
                TripStatus = mergedTripDto.TripStatus,
                FromOfficeId = fromOfficeId,
                ToOfficeId = toOfficeId,
                FlightInformations = mergedTripDto.FlightInformations,
                RentalCarInformations = mergedTripDto.RentalCarInformations,
                UserIds = mergedTripDto.Users.Select(user => user.Id).ToList(),
                OrganizerId = mergedTripDto.OrganizerId
            };
        }
    }
}
