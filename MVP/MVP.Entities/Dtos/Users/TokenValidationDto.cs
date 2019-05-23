using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.Users
{
    public class TokenValidationDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
