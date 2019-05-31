using MVP.Entities.Dtos.Emails;

namespace MVP.EmailService.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(EmailMessageDto emailMessageDto);
    }
}
