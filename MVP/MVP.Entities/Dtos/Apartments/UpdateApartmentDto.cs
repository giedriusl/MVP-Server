using MVP.Entities.Dtos.Apartments.ApartmentRooms;
using System.Collections.Generic;
using MVP.Entities.Dtos.Locations;
using MVP.Entities.Entities;

namespace MVP.Entities.Dtos.Apartments
{
    public class UpdateApartmentDto : ApartmentDto
    {
        /*public List<CreateApartmentRoomDto> Rooms { get; set; } = new List<CreateApartmentRoomDto>();

        public static Apartment ToEntity(UpdateApartmentDto apartment)
        {
            return new Apartment
            {
                Title = apartment.Title,
                BedCount = apartment.BedCount,
                Location = LocationCreateDto.ToEntity(apartment.Location),
                Rooms = CreateApartmentRoomDto.ToEntity(apartment.Rooms).
            };
        }*/

    }
}
