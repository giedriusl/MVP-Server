using MVP.Entities.Dtos.Locations;
using MVP.Entities.Entities;
using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.Offices
{
    public class CreateOfficeDto : OfficeDto
    {
        public int Id { get; set; }

        [Required]
        public LocationDto Location { get; set; }
        public static Office ToEntity(CreateOfficeDto newOfficeDto)
        {
            var office = new Office
            {
                Name = newOfficeDto.Name,
                Location = LocationDto.ToEntity(newOfficeDto.Location)
            };

            return office;
        }

        public static CreateOfficeDto ToDto(Office entity)
        {
            return new CreateOfficeDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Location = LocationDto.ToDto(entity.Location)
            };
        }
    }
}
