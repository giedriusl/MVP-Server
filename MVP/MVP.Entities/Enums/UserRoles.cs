using System.ComponentModel;

namespace MVP.Entities.Enums
{
    public enum UserRoles
    {
        [Description("User")]
        User = 0,

        [Description("Organizer")]
        Organizer = 1,

        [Description("Administrator")]
        Administrator = 2
    }
}
