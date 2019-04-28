namespace MVP.Entities.Entities
{
    public class ApartmentRoom
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int RoomNumber { get; set; }
        public int AppartmentId { get; set; }
        public Apartment Apartment{ get; set; }
        public int BedCount{ get; set; }
    }
}
