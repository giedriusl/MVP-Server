using MVP.Entities.Entities;

namespace MVP.Entities.Dtos.Apartments.ApartmentRooms
{
    public class ApartmentRoomDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int RoomNumber { get; set; }
        public int BedCount { get; set; }

        public static ApartmentRoomDto ToDto(ApartmentRoom room)
        {
            return new ApartmentRoomDto
            {
                Id = room.Id,
                Title = room.Title,
                RoomNumber = room.RoomNumber,
                BedCount = room.BedCount
            };
        }
    }
}
