using System.Collections.Generic;
using MVP.Entities.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MVP.Entities.Dtos.Users;
using MVP.Entities.Entities;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(CreateUserDto createUserDto);
        Task<string> LoginAsync(UserDto userDto);
        Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task SendResetPasswordLinkAsync(string email);
        Task CreateUsersCalendarFromFileAsync(IFormFile file);
    }
}
