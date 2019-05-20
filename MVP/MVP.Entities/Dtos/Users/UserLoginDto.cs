using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.Users
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(256)]
        public string Password { get; set; }
    }
}
