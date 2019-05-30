using MVP.Entities.Dtos.Offices;
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
        public new OfficeViewDto FromOffice { get; set; }

        [Required]
        public new OfficeViewDto ToOffice { get; set; }

        [Required]
        public new IEnumerable<UserDto> Users { get; set; }


        public new static TripViewDto ToDto(Trip trip)
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
                Users = trip.UserTrips.Select(userTrip => UserDto.ToDto(userTrip.User)),
            };
        }
    }
}
