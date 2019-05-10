using MVP.Entities.Models;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<string> CreateAsync(NewUserDto newUserDto);
        Task<string> LoginAsync(UserLoginDto userLoginDto);
    }
}
