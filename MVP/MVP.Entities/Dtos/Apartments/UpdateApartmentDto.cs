using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.Apartments
{
    public class UpdateApartmentDto : ApartmentDto
    {
        [Required]
        public int Id { get; set; }
    }
}
