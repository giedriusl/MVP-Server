using System.Threading.Tasks;
using MVP.Entities.Entities;

namespace MVP.BusinessLogic.Helpers.TokenGenerator
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(User user);
    }
}
