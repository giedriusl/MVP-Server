namespace MVP.BusinessLogic.Helpers.UrlBuilder
{
    public interface IUrlBuilder
    {
        string BuildPasswordResetLink(string token, string email);
        string BuildTripConfirmationLink(int tripId);
    }
}
