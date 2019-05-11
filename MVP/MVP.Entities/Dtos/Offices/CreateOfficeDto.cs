using MVP.Entities.Dtos.Locations;
using MVP.Entities.Dtos.Office;


namespace MVP.Entities.Dtos.Offices
{
    class CreateOfficeDto : OfficeDto
    {
        public static Entities.Office ToEntity(OfficeDto newOfficeDto)
        {
            var office = new Entities.Office
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
