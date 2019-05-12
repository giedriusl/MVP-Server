using System.Threading.Tasks;
using MVP.Entities.Dtos;
using MVP.Entities.Entities;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(CreateUserDto newUserDto);
        Task<string> LoginAsync(UserLoginDto userLoginDto);
        Task ResetPassword(ResetPasswordDto resetPasswordDto);
        Task SendResetPasswordLink(string email);
    }
}
