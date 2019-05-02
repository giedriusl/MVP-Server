using MVP.Entities.Entities;

namespace MVP.BusinessLogic.Helpers.TokenGenerator
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
    }
}
