using System.Collections.Generic;
using System.Linq;
using MVP.Entities.Dtos.Apartments.ApartmentRooms;
using MVP.Entities.Dtos.Locations;
using MVP.Entities.Entities;

namespace MVP.Entities.Dtos.Apartments
{
    public class CreateApartmentDto : ApartmentDto
    {
        public LocationCreateDto Location { get; set; }
        public List<SubmitApartmentRoomDto> Rooms { get; set; } = new List<SubmitApartmentRoomDto>();
        public int Id { get; set; }
        public static Apartment ToEntity(CreateApartmentDto createApartment)
        {
            return new Apartment
            {
                Title = createApartment.Title,
                BedCount = createApartment.BedCount,
                Location = LocationCreateDto.ToEntity(createApartment.Location),
                Rooms = createApartment.Rooms.Select(SubmitApartmentRoomDto.ToEntity).ToList()
            };
        }

        public static CreateApartmentDto ToDto(Apartment apartment)
        {
            return new CreateApartmentDto
            {
                Id = apartment.Id,
                Title = apartment.Title,
                BedCount = apartment.BedCount,
                Location = LocationCreateDto.ToDto(apartment.Location),
                Rooms = apartment.Rooms.Select(SubmitApartmentRoomDto.ToDto).ToList()
            };
        }
    }
}
