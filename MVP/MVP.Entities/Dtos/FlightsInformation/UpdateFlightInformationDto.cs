using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.FlightsInformation
{
    public class UpdateFlightInformationDto : FlightInformationDto
    {
        [Required]
        public int Id { get; set; }
    }
}
