namespace MVP.Entities.Entities
{
    public class UserTrip
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
