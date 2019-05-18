using Microsoft.Extensions.Configuration;

namespace MVP.BusinessLogic.Helpers.UrlBuilder
{
    public class UrlBuilder : IUrlBuilder
    {
        private readonly IConfiguration _configuration;

        public UrlBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string BuildPasswordResetLink(string token, string email)
        {
            var invitationUrl = _configuration["Urls:PasswordResetUrl"];
            var urlWithToken = invitationUrl + $"?{token}&{email}";

            return urlWithToken;
        }
    }
}
