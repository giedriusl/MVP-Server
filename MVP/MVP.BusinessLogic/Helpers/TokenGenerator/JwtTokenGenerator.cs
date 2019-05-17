using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MVP.Entities.Dtos.Token;
using MVP.Entities.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MVP.BusinessLogic.Helpers.TokenGenerator
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public JwtTokenGenerator(IConfiguration configuration, 
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<TokenWithClaimsPrincipal> GenerateTokenAsync(User user)
        {
            var claims = await GetValidClaims(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:JwtKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Token:JwtExpireDays"]));
            var notBefore = DateTime.Now;

            var token = new JwtSecurityToken(
                _configuration["Token:Issuer"],
                _configuration["Token:Audience"],
                claims,
                notBefore,
                expires,
                credentials
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenWithClaimsPrincipal
            {
                AccessToken = accessToken,
                ClaimsPrincipal = ClaimsPrincipalFactory.CreatePrincipal(claims),
                AuthProperties = CreateAuthProperties(accessToken)
            };
        }

        private static AuthenticationProperties CreateAuthProperties(string accessToken)
        {
            var authProps = new AuthenticationProperties();
            authProps.StoreTokens(
                new[]
                {
                    new AuthenticationToken()
                    {
                        Name = "jwt",
                        Value = accessToken
                    }
                });

            return authProps;
        }

        private async Task<List<Claim>> GetValidClaims(User user)
        {
            var option = new IdentityOptions();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(option.ClaimsIdentity.UserIdClaimType, user.Id),
                new Claim(option.ClaimsIdentity.UserNameClaimType, user.UserName)
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.AddRange(userClaims);
            foreach (var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);

                //TODO: throw exception when role does not exist
                if (role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));

                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }

            return claims;
        }
    }
}
