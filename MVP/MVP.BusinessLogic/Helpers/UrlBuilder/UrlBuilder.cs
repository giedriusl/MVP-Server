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

        public string BuildInvitationUrl(string token)
        {
            var invitationUrl = _configuration["Urls:InvitationUrl"];
            var urlWithToken = invitationUrl + $"?{token}";

            return urlWithToken;
        }
    }
}
