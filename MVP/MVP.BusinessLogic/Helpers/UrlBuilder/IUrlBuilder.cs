namespace MVP.BusinessLogic.Helpers.UrlBuilder
{
    public interface IUrlBuilder
    {
        string BuildPasswordResetLink(string token, string email);
    }
}
