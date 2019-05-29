using MVP.Entities.Entities;

namespace MVP.Entities.Dtos.TripInfo
{
    public class TripApartmentInfoDto
    {
        public int TripId { get; set; }
        public int ApartmentRoomId { get; set; }
        public string UserId { get; set; }
        public int CalendarId { get; set; }

        public static TripApartmentInfoDto ToDto(TripApartmentInfo tripApartmentInfo)
        {
            var dto = new TripApartmentInfoDto
            {
                TripId = tripApartmentInfo.TripId,
                ApartmentRoomId = tripApartmentInfo.ApartmentRoomId,
                UserId = tripApartmentInfo.UserId,
                CalendarId = tripApartmentInfo.CalendarId
            };

            return dto;
        }
    }
}
