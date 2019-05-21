using MVP.Entities.Enums;

namespace MVP.Entities.Dtos.Trips
{
    public class TripStatusDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static TripStatusDto ToDto(TripStatus status)
        {
            return new TripStatusDto
            {
                Id = (int)status,
                Name = status.ToString()
            };
        }
    }
}
