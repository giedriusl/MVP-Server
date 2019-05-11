using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MVP.Entities.Dtos.Apartments.ApartmentRooms;
using MVP.Entities.Dtos.Locations;

namespace MVP.Entities.Dtos.Apartments
{
    public class ApartmentCreateDto
    {
        [Required]
        [StringLength(256)]
        public string Title { get; set; }
        public int? OfficeId { get; set; }

        [Required]
        public int BedCount { get; set; }

        [Required]
        public int LocationId { get; set; }
        //public Office Office { get; set; }
        public LocationCreateDto Location { get; set; }
        public List<ApartmentRoomCreateDto> Rooms { get; set; } = new List<ApartmentRoomCreateDto>();
    }
}
