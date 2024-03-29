﻿using Microsoft.AspNetCore.Hosting;
using MimeKit;
using MVP.EmailService.Interfaces;
using MVP.Entities.Dtos.Emails;
using System.IO;

namespace MVP.EmailService
{
    public class EmailManager : IEmailManager
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IEmailSender _emailSender;

        private const string CreateAccountPasswordPath = "EmailTemplates\\CreateAccountPassword.html";
        private const string TripConfirmationPath = "EmailTemplates\\TripConfirmation.html";
        public const string CreateAccountPasswordSubject = "Create Password";
        public const string TripConfirmationSubject = "Please confirm the trip";


        public EmailManager(IEmailSender emailSender, IHostingEnvironment hostingEnvironment)
        {
            _emailSender = emailSender;
            _hostingEnvironment = hostingEnvironment;
        }

        public void SendInvitationEmail(string email, string url)
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath, CreateAccountPasswordPath);
            var body = BuildBody(path);
            var messageBody = string.Format(body.HtmlBody, url);

            var emailMessage = new EmailMessageDto
            {
                ToAddress = email,
                Subject = CreateAccountPasswordSubject,
                Body = messageBody
            };

            _emailSender.SendEmail(emailMessage);
        }

        public void SendTripConfirmationEmail(string email, string url)
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath, TripConfirmationPath);
            var body = BuildBody(path);

            var messageBody = string.Format(body.HtmlBody, url);

            var emailMessage = new EmailMessageDto
            {
                ToAddress = email,
                Subject = TripConfirmationSubject,
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
