namespace MVP.Entities.Entities
{
    public class Location
    {
        public Location()
        {
            //for ef
        }

        public int Id { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string Address { get; set; }
    }
}
