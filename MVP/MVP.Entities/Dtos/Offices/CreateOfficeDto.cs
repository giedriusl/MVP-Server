using MVP.Entities.Dtos.Locations;
using MVP.Entities.Entities;
using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.Offices
{
    public class CreateOfficeDto : OfficeDto
    {
        public new int Id { get; set; }

        [Required]
        public LocationDto Location { get; set; }


        public static Office ToEntity(CreateOfficeDto newOfficeDto)
        {
            var office = new Office
            {
                Name = newOfficeDto.Name
            };

            return office;
        }

        public new static CreateOfficeDto ToDto(Office office)
        {
            return new CreateOfficeDto
            {
                Id = office.Id,
                Name = office.Name,
                Location = LocationDto.ToDto(office.Location)
            };
        }
    }
}
