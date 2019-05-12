using System.Linq;
using MVP.Entities.Dtos.Apartments.ApartmentRooms;
using MVP.Entities.Dtos.Locations;
using MVP.Entities.Entities;

namespace MVP.Entities.Dtos.Apartments
{
    public class CreateApartmentDto : ApartmentDto
    {
        public static Apartment ToEntity(CreateApartmentDto createApartment)
        {
            return new Apartment
            {
                Title = createApartment.Title,
                BedCount = createApartment.BedCount,
                Location = LocationCreateDto.ToEntity(createApartment.Location),
                Rooms = createApartment.Rooms.Select(CreateApartmentRoomDto.ToEntity).ToList()
            };
        }
    }
}
