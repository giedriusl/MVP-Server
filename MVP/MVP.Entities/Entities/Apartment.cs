namespace MVP.Entities.Entities
{
    public class Apartment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int OfficeId { get; set; }
        public int BedCount { get; set; }
        public int LocationId { get; set; }
        public Office Office { get; set; }
        public Location Location { get; set; }
    }
}
