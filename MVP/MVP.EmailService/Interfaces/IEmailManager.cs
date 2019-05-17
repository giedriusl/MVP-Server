namespace MVP.EmailService.Interfaces
{
    public interface IEmailManager
    {
        void SendConfirmationEmail(string email, string url);

    }
}
