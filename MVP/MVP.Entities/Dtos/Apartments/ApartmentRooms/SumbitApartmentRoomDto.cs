using MVP.Entities.Entities;
using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.Apartments.ApartmentRooms
{
    public class SubmitApartmentRoomDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [Required]
        public int RoomNumber { get; set; }

        [Required]
        public int BedCount { get; set; }

        public static ApartmentRoom ToEntity(SubmitApartmentRoomDto submitRoom)
        {
            return new ApartmentRoom
            {
                Title = submitRoom.Title,
                RoomNumber = submitRoom.RoomNumber,
                BedCount = submitRoom.BedCount
            };
        }

        public static SubmitApartmentRoomDto ToDto(ApartmentRoom room)
        {
            return new SubmitApartmentRoomDto
            {
                Id = room.Id,
                Title = room.Title,
                RoomNumber = room.RoomNumber,
                BedCount = room.BedCount
            };
        }
    }
}
