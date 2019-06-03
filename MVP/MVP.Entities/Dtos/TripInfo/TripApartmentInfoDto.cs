using MVP.Entities.Entities;
using System;

namespace MVP.Entities.Dtos.TripInfo
{
    public class TripApartmentInfoDto
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public int ApartmentRoomId { get; set; }
        public string UserId { get; set; }
        public int CalendarId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ApartmentName { get; set; }
        public int RoomNumber { get; set; }

        public DateTimeOffset Start { get; set; }

        public DateTimeOffset End { get; set; }

        public static TripApartmentInfoDto ToDto(TripApartmentInfo tripApartmentInfo)
        {
            var dto = new TripApartmentInfoDto
            {
                Id = tripApartmentInfo.Id,
                TripId = tripApartmentInfo.TripId,
                ApartmentRoomId = tripApartmentInfo.ApartmentRoomId,
                UserId = tripApartmentInfo.UserId,
                CalendarId = tripApartmentInfo.CalendarId,
                Start = tripApartmentInfo.Calendar.Start,
                End = tripApartmentInfo.Calendar.End
            };

            return dto;
        }
    }
}
