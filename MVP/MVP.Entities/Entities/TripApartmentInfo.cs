namespace MVP.Entities.Entities
{
    public class TripApartmentInfo
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public int ApartmentRoomId { get; set; }
        public string UserId { get; set; }
        public int CalendarId { get; set; }
        public virtual Calendar Calendar { get; set; }
    }
}
