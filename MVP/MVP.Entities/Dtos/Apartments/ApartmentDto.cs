using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MVP.Entities.Dtos.Apartments.ApartmentRooms;
using MVP.Entities.Dtos.Locations;

namespace MVP.Entities.Dtos.Apartments
{
    public class ApartmentDto
    {
        [Required]
        [StringLength(256)]
        public string Title { get; set; }
        public int? OfficeId { get; set; }

        [Required]
        public int BedCount { get; set; }
        public List<CreateApartmentRoomDto> Rooms { get; set; } = new List<CreateApartmentRoomDto>();

        //public Office Office { get; set; }
        public LocationCreateDto Location { get; set; }
    }
}
