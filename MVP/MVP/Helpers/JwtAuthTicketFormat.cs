using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;

namespace MVP.Helpers
{
    public class JwtAuthTicketFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private const string Algorithm = SecurityAlgorithms.HmacSha256;
        private readonly TokenValidationParameters _validationParameters;
        private readonly IDataSerializer<AuthenticationTicket> _ticketSerializer;
        private readonly IDataProtector _dataProtector;

        private const string TokenName = "jwt";

        public JwtAuthTicketFormat(TokenValidationParameters validationParameters,
            IDataSerializer<AuthenticationTicket> ticketSerializer,
            IDataProtector dataProtector)
        {
            this._validationParameters = validationParameters ??
                throw new ArgumentNullException($"{nameof(validationParameters)} cannot be null");
            this._ticketSerializer = ticketSerializer ??
                throw new ArgumentNullException($"{nameof(ticketSerializer)} cannot be null"); ;
            this._dataProtector = dataProtector ??
                throw new ArgumentNullException($"{nameof(dataProtector)} cannot be null");
        }

        public AuthenticationTicket Unprotect(string protectedText)
            => Unprotect(protectedText, null);
        
        public AuthenticationTicket Unprotect(string protectedText, string purpose)
        {
            var authTicket = _ticketSerializer.Deserialize(
                _dataProtector.Unprotect(
                    Base64UrlTextEncoder.Decode(protectedText)));

            var embeddedJwt = authTicket
                .Properties?
                .GetTokenValue(TokenName);

            try
            {
                new JwtSecurityTokenHandler()
                    .ValidateToken(embeddedJwt, _validationParameters, out var token);

                if (!(token is JwtSecurityToken jwt))
                {
                    throw new SecurityTokenValidationException("JWT token was found to be invalid");
                }

                if (!jwt.Header.Alg.Equals(Algorithm, StringComparison.Ordinal))
                {
                    throw new ArgumentException($"Algorithm must be '{Algorithm}'");
                }
            }
            catch (Exception)
            {
                return null;
            }

            return authTicket;
        }

        public string Protect(AuthenticationTicket data) => Protect(data, null);
       
        public string Protect(AuthenticationTicket data, string purpose)
        {
            var array = _ticketSerializer.Serialize(data);

            return Base64UrlTextEncoder.Encode(_dataProtector.Protect(array));
        }
    }
}
