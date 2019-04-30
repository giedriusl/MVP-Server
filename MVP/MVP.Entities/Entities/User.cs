using Microsoft.AspNetCore.Identity;

namespace MVP.Entities.Entities
{
    public class User : IdentityUser
    {
        public int WorkerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
