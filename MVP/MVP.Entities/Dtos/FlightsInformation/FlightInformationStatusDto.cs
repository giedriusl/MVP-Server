using MVP.Entities.Enums;

namespace MVP.Entities.Dtos.FlightsInformation
{
    public class FlightInformationStatusDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static FlightInformationStatusDto ToDto(FlightInformationStatus status)
        {
            return new FlightInformationStatusDto
            {
                Id = (int)status,
                Name = status.ToString()
            };
        }
    }
}
