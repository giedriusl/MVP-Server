using System.Threading.Tasks;
using MVP.Entities.Dtos;
using MVP.Entities.Dtos.Token;

namespace MVP.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<TokenWithClaimsPrincipal> CreateAsync(CreateUserDto newUserDto);
        Task<TokenWithClaimsPrincipal> LoginAsync(UserLoginDto userLoginDto);
    }
}
