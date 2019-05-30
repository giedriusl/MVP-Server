using MVP.Entities.Entities;
using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.Offices
{
    public class OfficeDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public static OfficeDto ToDto(Office office)
        {
            return new OfficeDto
            {
                Id = office.Id,
                Name = office.Name
            };
        }
    }
}
