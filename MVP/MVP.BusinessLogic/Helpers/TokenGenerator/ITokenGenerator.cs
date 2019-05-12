using MVP.Entities.Entities;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Helpers.TokenGenerator
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(User user);
    }
}
