using Microsoft.AspNetCore.Http;
using MVP.Entities.Dtos;
using MVP.Entities.Dtos.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<CreateUserDto> CreateAsync(CreateUserDto createUserDto);
        Task<string> LoginAsync(UserLoginDto userDto);
        Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task SendResetPasswordLinkAsync(string email);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(string userId);
        Task UploadUsersAsync(IFormFile file);
        Task UploadUsersCalendarAsync(IFormFile file);
        Task<UserDto> GetUserByEmail(string email);

        IEnumerable<UserRolesDto> GetUserRoles();
        Task ValidateResetPasswordToken(string email, string token);
    }
}
