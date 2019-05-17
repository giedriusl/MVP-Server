using System.Threading.Tasks;
using MVP.Entities.Dtos.Token;
using MVP.Entities.Entities;

namespace MVP.BusinessLogic.Helpers.TokenGenerator
{
    public interface ITokenGenerator
    {
        Task<TokenWithClaimsPrincipal> GenerateTokenAsync(User user);
    }
}
