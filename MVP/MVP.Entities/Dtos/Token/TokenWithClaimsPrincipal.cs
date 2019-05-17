using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace MVP.Entities.Dtos.Token
{
    public class TokenWithClaimsPrincipal
    {
        public string AccessToken { get; set; }

        public ClaimsPrincipal ClaimsPrincipal { get; set; }

        public AuthenticationProperties AuthProperties { get; set; }
    }
}
