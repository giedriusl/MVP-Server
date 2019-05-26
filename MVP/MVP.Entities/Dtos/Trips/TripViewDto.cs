using MVP.Entities.Dtos.Offices;
using MVP.Entities.Entities;
using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.Trips
{
    public class TripViewDto : TripDto
    {
        [Required]
        public OfficeViewDto FromOffice { get; set; }

        [Required]
        public OfficeViewDto ToOffice { get; set; }


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
                ToOffice = OfficeViewDto.ToDto(trip.ToOffice)
            };
        }
    }
}
