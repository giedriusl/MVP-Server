﻿using System.IO;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using MVP.EmailService.Interfaces;
using MVP.Entities.Dtos.Emails;
using MVP.Entities.Helpers;

namespace MVP.EmailService
{
    public class EmailManager : IEmailManager
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IEmailSender _emailSender;

        public EmailManager(IEmailSender emailSender, IHostingEnvironment hostingEnvironment)
        {
            _emailSender = emailSender;
            _hostingEnvironment = hostingEnvironment;
        }

        public void SendInvitationEmail(string email, string url)
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath, Email.CreateAccountPasswordPath);
            var body = BuildBody(path);
            var messageBody = string.Format(body.HtmlBody, url);

            var emailMessage = new EmailMessageDto
            {
                ToAddress = email,
                Subject = Email.CreateAccountPasswordSubject,
                Body = messageBody
            };

            _emailSender.SendEmail(emailMessage);
        }

        public BodyBuilder BuildBody(string pathToFile)
        {
            var bodyBuilder = new BodyBuilder();

            using (var sourceReader = File.OpenText(pathToFile))
            {
                bodyBuilder.HtmlBody = sourceReader.ReadToEnd();
            }

            return bodyBuilder;
        }
    }
}
