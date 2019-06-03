using MVP.Entities.Dtos.Locations;
using MVP.Entities.Entities;

namespace MVP.Entities.Dtos.Apartments
{
    public class ApartmentViewDto : ApartmentDto
    {
        public int Id { get; set; }
        public LocationDto Location { get; set; }
        public string OfficeName { get; set; }

        public static ApartmentViewDto ToDto(Apartment apartment)
        {
            return new ApartmentViewDto
            {
                Id = apartment.Id,
                Title = apartment.Title,
                OfficeId = apartment.OfficeId,
                Location = LocationDto.ToDto(apartment.Location),
                OfficeName = apartment.Office.Name
            };
        }
    }
}
