using System.ComponentModel.DataAnnotations;
using MVP.Entities.Entities;

namespace MVP.Entities.Dtos.Users
{
    public class UserDto
    {
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

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(256)]
        public string Password { get; set; }


        public static UserDto ToDto(User user)
        {
            return new UserDto
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email
            };
        }
    }
}
