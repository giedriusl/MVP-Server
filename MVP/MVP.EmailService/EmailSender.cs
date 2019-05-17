using Microsoft.Extensions.Configuration;
using MVP.EmailService.Interfaces;
using MVP.Entities.Dtos.Emails;
using System.Net;
using System.Net.Mail;

namespace MVP.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(EmailMessageDto emailMessageDto)
        {
            using (var smtpClient = new SmtpClient())
            {
                var credentials = new NetworkCredential
                {
                    UserName = _configuration["Email:Email"],
                    Password = _configuration["Email:Password"]
                };

                smtpClient.Credentials = credentials;
                smtpClient.Host = _configuration["Email:Host"];
                smtpClient.Port = int.Parse(_configuration["Email:Port"]);
                smtpClient.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(emailMessageDto.ToAddress));
                    emailMessage.From = new MailAddress(_configuration["Email:Email"]);
                    emailMessage.Subject = emailMessageDto.Subject;
                    emailMessage.Body = emailMessageDto.Body;
                    emailMessage.IsBodyHtml = true;

                    smtpClient.Send(emailMessage);
                }
            }
        }
    }
}
