using MVP.Entities.Entities;
using MVP.Entities.Enums;

namespace MVP.Entities.Dtos.Users
{
    public class CreateUserDto : UserDto
    {
        public UserRoles Role { get; set; }

        public static User ToEntity(CreateUserDto newUserDto)
        {
            var user = new User
            {
                Name = newUserDto.Name,
                Surname = newUserDto.Surname,
                Email = newUserDto.Email,
                UserName = newUserDto.Email
            };

            return user;
        }
    }
}
