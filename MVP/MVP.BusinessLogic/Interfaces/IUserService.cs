using System.Collections.Generic;
using MVP.Entities.Dtos;
using MVP.Entities.Dtos.Users;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
    }
}
