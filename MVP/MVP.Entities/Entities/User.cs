using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MVP.Entities.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<UserTrip> UserTrips { get; set; }
        public virtual ICollection<Calendar> Calendars { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
    }
}
