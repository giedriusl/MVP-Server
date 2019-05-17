using MimeKit;
using MVP.EmailService.Interfaces;
using MVP.Entities.Dtos.Emails;

namespace MVP.EmailService
{
    public class EmailManager : IEmailManager
    {
        private readonly IEmailSender _emailSender;

        public EmailManager(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        private const string CreatePasswordPath = "C://Users//giedr//source//repos//MVP-Server//MVP//MVP.EmailService//EmailTemplates//CreateAccountPassword.html";

        public void SendInvitationEmail(string email, string url)
        {
            var body = BuildBody(CreatePasswordPath);
            var messageBody = string.Format(body.HtmlBody, url);

            var emailMessage = new EmailMessageDto
            {
                ToAddress = email,
                Subject = "Create Password",
                Body = messageBody
            };

            _emailSender.SendEmail(emailMessage);
        }

        public BodyBuilder BuildBody(string pathToFile)
        {
            var bodyBuilder = new BodyBuilder();

            using (var sourceReader = System.IO.File.OpenText(CreatePasswordPath))
            {
                bodyBuilder.HtmlBody = sourceReader.ReadToEnd();
            }

            return bodyBuilder;
        }
    }
}
