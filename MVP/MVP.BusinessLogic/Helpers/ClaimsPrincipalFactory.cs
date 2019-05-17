using System.Collections.Generic;
using System.Security.Claims;

namespace MVP.BusinessLogic.Helpers
{
    public class ClaimsPrincipalFactory
    {
        public static ClaimsPrincipal CreatePrincipal(IEnumerable<Claim> claims, string authenticationType = null, string roleType = null)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.AddIdentity(new ClaimsIdentity(claims,
                string.IsNullOrWhiteSpace(authenticationType) ? "Password" : authenticationType,
                ClaimTypes.Name,
                string.IsNullOrWhiteSpace(roleType) ? "Recipient" : roleType));

            return claimsPrincipal;
        }
    }
}
