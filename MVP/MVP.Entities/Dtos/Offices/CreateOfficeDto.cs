using MVP.Entities.Dtos.Locations;
using MVP.Entities.Entities;

namespace MVP.Entities.Dtos.Offices
{
    class CreateOfficeDto : OfficeDto
    {
        public static Office ToEntity(OfficeDto newOfficeDto)
        {
            var office = new Office
            {
                Name = newOfficeDto.Name,
                Location = CreateLocationDto.ToEntity(newOfficeDto.LocationDto),
                // TODO ApartmentsDto
                //Apartments = CreateApartmentsDto.ToEntity()
            };

            return office;
        }
    }
}
