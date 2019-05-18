﻿using System.Collections.Generic;
using System.Linq;
using MVP.Entities.Dtos.Apartments.ApartmentRooms;
using MVP.Entities.Dtos.Locations;
using MVP.Entities.Entities;

namespace MVP.Entities.Dtos.Apartments
{
    public class CreateApartmentDto : ApartmentDto
    {
        public int Id { get; set; }
        public LocationDto Location { get; set; }
        public List<CreateApartmentRoomDto> Rooms { get; set; } = new List<CreateApartmentRoomDto>();
        public static Apartment ToEntity(CreateApartmentDto createApartment)
        {
            return new Apartment
            {
                Title = createApartment.Title,
                Rooms = createApartment.Rooms.Select(CreateApartmentRoomDto.ToEntity).ToList()
            };
        }

        public static CreateApartmentDto ToDto(Apartment apartment)
        {
            return new CreateApartmentDto
            {
                Id = apartment.Id,
                Title = apartment.Title,
                Location = LocationDto.ToDto(apartment.Location),
                Rooms = apartment.Rooms.Select(CreateApartmentRoomDto.ToDto).ToList()
            };
        }
    }
}
