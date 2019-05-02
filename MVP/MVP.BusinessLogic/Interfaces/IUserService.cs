using System.Threading.Tasks;
using MVP.Entities.Entities;
using MVP.Entities.Models;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<string> CreateAsync(NewUserDto newUserDto);
        Task<string> LoginAsync(UserLoginDto userLoginDto);
    }
}
