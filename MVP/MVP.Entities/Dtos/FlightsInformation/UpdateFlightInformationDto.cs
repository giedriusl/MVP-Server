using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.FlightsInformation
{
    public class UpdateFlightInformationDto : FlightInformationDto
    {
        [Required]
        public override int Id { get; set; }
    }
}
