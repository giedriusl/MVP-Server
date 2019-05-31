using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.RentalCarsInformation
{
    public class UpdateRentalCarInformationDto :  RentalCarInformationDto
    {
        [Required]
        public override int Id { get; set; }
    }
}
