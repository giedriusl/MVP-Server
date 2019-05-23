namespace MVP.EmailService.Interfaces
{
    public interface IEmailManager
    {
        void SendInvitationEmail(string email, string url);
        void SendTripConfirmationEmail(string email, string url);
    }
}
