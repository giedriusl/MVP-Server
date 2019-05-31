using MVP.Entities.Dtos.Locations;
using MVP.Entities.Entities;

namespace MVP.Entities.Dtos.Offices
{
    public class OfficeViewDto : OfficeDto
    {
        public new int Id { get; set; }
        public LocationDto Location { get; set; }

        public new static OfficeViewDto ToDto(Office office)
        {
            return new OfficeViewDto
            {
                Id = office.Id,
                Name = office.Name,
                Location = LocationDto.ToDto(office.Location)
            };
        }
    }
}
