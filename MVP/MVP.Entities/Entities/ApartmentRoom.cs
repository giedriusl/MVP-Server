using System.Collections.Generic;

namespace MVP.Entities.Entities
{
    public class ApartmentRoom
    {
        public int Id { get; private set; }
        public string Title { get; set; }
        public int RoomNumber { get; set; }
        public int AppartmentId { get; set; }
        public int BedCount{ get; set; }
        public virtual List<Calendar> Calendars { get; set; }
    }
}
