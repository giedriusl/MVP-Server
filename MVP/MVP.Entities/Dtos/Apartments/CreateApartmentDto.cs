using MVP.Entities.Dtos.Locations;
using MVP.Entities.Entities;
using System.Linq;

namespace MVP.Entities.Dtos.Apartments
{
    public class CreateApartmentDto : ApartmentDto
    {
        public int Id { get; set; }
        public int? RoomNumber { get; set; }
        public int? RoomId { get; set; }
        public LocationDto Location { get; set; }


        public static Apartment ToEntity(CreateApartmentDto createApartment)
        {
            return new Apartment
            {
                Title = createApartment.Title
            };
        }

        public static CreateApartmentDto ToDto(Apartment apartment)
        {
            return new CreateApartmentDto
            {
                Id = apartment.Id,
                Title = apartment.Title,
                OfficeId = apartment.OfficeId,
                Location = LocationDto.ToDto(apartment.Location),
                RoomId = apartment.Rooms.FirstOrDefault().Id
            };
        }
    }
}
