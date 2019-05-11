using System.Threading.Tasks;
using MVP.Entities.Dtos;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<string> CreateAsync(NewUserDto newUserDto);
        Task<string> LoginAsync(UserLoginDto userLoginDto);
    }
}
