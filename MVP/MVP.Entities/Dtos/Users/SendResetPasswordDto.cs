using System.ComponentModel.DataAnnotations;

namespace MVP.Entities.Dtos.Users
{
    public class SendResetPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
