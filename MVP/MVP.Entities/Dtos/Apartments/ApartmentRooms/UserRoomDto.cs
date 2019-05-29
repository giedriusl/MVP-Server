using System;
using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.Apartments.ApartmentRooms
{
    public class UserRoomDto
    {
        [Required]
        public int TripId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int ApartmentId { get; set; }
        [Required]
        public int ApartmentRoomId { get; set; }
        [Required]
        public DateTimeOffset Start { get; set; }
        [Required]
        public DateTimeOffset End { get; set; }

        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string ApartmentName { get; set; }
        public int RoomNumber { get; set; }
    }
}
