using MVP.Entities.Dtos;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(CreateUserDto createUserDto);
        Task<string> LoginAsync(UserDto userDto);
        Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task SendResetPasswordLinkAsync(string email);
    }
}
