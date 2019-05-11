using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos
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
    }
}
