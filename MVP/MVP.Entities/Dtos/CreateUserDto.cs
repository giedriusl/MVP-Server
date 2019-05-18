﻿using MVP.Entities.Entities;
using MVP.Entities.Enums;

namespace MVP.Entities.Dtos
{
    public class CreateUserDto : UserDto
    {
        public UserRoles Role { get; set; }

        public static User ToEntity(CreateUserDto createUserDto)
        {
            var user = new User
            {
                Name = createUserDto.Name,
                Surname = createUserDto.Surname,
                Email = createUserDto.Email,
                UserName = createUserDto.Email
            };

            return user;
        }
    }
}