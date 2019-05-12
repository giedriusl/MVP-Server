namespace MVP.EmailService.Interfaces
{
    public interface IEmailManager
    {
        void SendInvitationEmail(string email, string url);

    }
}
