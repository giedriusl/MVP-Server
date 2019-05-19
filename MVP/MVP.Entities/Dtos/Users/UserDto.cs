using System.ComponentModel.DataAnnotations;
using MVP.Entities.Entities;

namespace MVP.Entities.Dtos.Users
{
    public class UserDto
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        public static UserDto ToDto(User user)
        {
            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email
            };

            return userDto;
        }
    }
}
