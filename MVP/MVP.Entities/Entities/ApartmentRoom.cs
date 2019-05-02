namespace MVP.Entities.Entities
{
    public class ApartmentRoom
    {
        public ApartmentRoom()
        {
            //for ef
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public int RoomNumber { get; private set; }
        public int AppartmentId { get; private set; }
        public int BedCount{ get; private set; }
    }
}
