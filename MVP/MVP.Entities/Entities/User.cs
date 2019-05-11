using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MVP.Entities.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual ICollection<UserTrip> UserTrips { get; set; } = new List<UserTrip>();
        public virtual ICollection<Calendar> Calendars { get; set; } = new List<Calendar>();

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
    }
}
