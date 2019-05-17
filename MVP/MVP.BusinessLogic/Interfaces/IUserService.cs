using System.Threading.Tasks;
using MVP.Entities.Dtos;
using MVP.Entities.Dtos.Users;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<string> CreateAsync(CreateUserDto newUserDto);
        Task<string> LoginAsync(UserLoginDto userLoginDto);
    }
}
