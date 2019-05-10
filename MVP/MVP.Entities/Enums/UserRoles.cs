using System.ComponentModel;

namespace MVP.Entities.Enums
{
    public enum UserRoles
    {
        [Description("User")]
        User = 1,

        [Description("Organizer")]
        Organizer = 2,

        [Description("Administrator")]
        Administrator = 3
    }
}
