using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.Trips
{
    public class UpdateTripDto : CreateTripDto
    {
        [Required]
        public override int Id { get; set; }
    }
}
