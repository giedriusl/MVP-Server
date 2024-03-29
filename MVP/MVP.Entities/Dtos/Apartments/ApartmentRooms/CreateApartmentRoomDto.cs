﻿using MVP.Entities.Entities;
using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.Apartments.ApartmentRooms
{
    public class CreateApartmentRoomDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [Required]
        public int RoomNumber { get; set; }

        [Required]
        public int BedCount { get; set; }

        public static ApartmentRoom ToEntity(CreateApartmentRoomDto createRoom)
        {
            return new ApartmentRoom
            {
                Title = createRoom.Title,
                RoomNumber = createRoom.RoomNumber,
                BedCount = createRoom.BedCount
            };
        }

        public static CreateApartmentRoomDto ToDto(ApartmentRoom room)
        {
            return new CreateApartmentRoomDto
            {
                Id = room.Id,
                Title = room.Title,
                RoomNumber = room.RoomNumber,
                BedCount = room.BedCount
            };
        }
    }
}
