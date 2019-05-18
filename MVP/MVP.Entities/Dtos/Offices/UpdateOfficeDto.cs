using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.Offices
{
    public class UpdateOfficeDto : OfficeDto
    {
        [Required]
        public int Id { get; set; }
    }
}
