using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MVP.Entities.Dtos.Calendars;
using MVP.Entities.Entities;

namespace MVP.Entities.Dtos.Apartments.ApartmentRooms
{
    public class ApartmentRoomCreateDto
    {
        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [Required]
        public int RoomNumber { get; set; }

        [Required]
        public int ApartmentId { get; set; }

        [Required]
        public int BedCount { get; set; }

        protected List<CalendarCreateDto> Calendars { get; set; } = new List<CalendarCreateDto>();

        public static ApartmentRoom ToEntity(ApartmentRoomCreateDto apartment)
        {
            return new ApartmentRoom();
        }
    }
}
