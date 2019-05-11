using System;
using System.Collections.Generic;
using System.Text;
using MVP.Entities.Entities;

namespace MVP.Entities.Dtos.Locations
{
    class CreateLocationDto : LocationDto
    {
        public static Location ToEntity(LocationDto newLocationDto)
        {
            var location = new Location
            {
                Address = newLocationDto.Address,
                City = newLocationDto.City,
                CountryCode = newLocationDto.CountryCode
            };

            return location;
        }
    }
}
