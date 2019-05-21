using MVP.Entities.Enums;

namespace MVP.Entities.Dtos.Users
{
    public class UserRolesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static UserRolesDto ToDto(UserRoles role)
        {
            return new UserRolesDto
            {
                Id = (int)role,
                Name = role.ToString()
            };
        }
    }
}
