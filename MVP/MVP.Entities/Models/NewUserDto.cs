using MVP.Entities.Entities;

namespace MVP.Entities.Models
{
    public class NewUserDto : UserDto
    {
        public static NewUserDto ToDto(User user)
        {
            var newUserDto = new NewUserDto
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email
            };

            return newUserDto;
        }

        public static User ToEntity(NewUserDto newUserDto)
        {
            var user = new User
            {
                Name = newUserDto.Name,
                Surname = newUserDto.Surname,
                Email = newUserDto.Email
            };

            return user;
        }
    }
}
