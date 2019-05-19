using MVP.Entities.Entities;
using MVP.Entities.Enums;

namespace MVP.Entities.Dtos.Users
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

        public new static CreateUserDto ToDto(User user)
        {
            var createUserDto = new CreateUserDto
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email
            };

            return createUserDto;
        }
    }
}
