using Microsoft.Extensions.Options;
using Shared.Core.Interfaces;
using Shared.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly SMTPConfig _SMTPConfig;
        private const string templatePath = @"./EmailTemplate/{0}.html";

        public EmailService(IOptions<SMTPConfig> options)
        {
            _SMTPConfig = options.Value;
        }
        public async Task SendConfirmationEmail(UserEmailOptions userEmailOptions)
        {
            var templateBody = GetEmailBody("ConfirmEmail");
            userEmailOptions.Body = UpdatePlaceHolders(templateBody, userEmailOptions.PlaceHolders);
            await SendEmail(userEmailOptions);
        }

        private async Task SendEmail(UserEmailOptions userEmail)
        {
            var mailMessage = new MailMessage()
            {
                Subject = userEmail.Subject,
                Body = userEmail.Body,
                From = new MailAddress(_SMTPConfig.SenderAddress, _SMTPConfig.SenderDisplayName),
                IsBodyHtml = _SMTPConfig.IsBodyHTML
            };

            foreach (var email in userEmail.ToEmails)
            {
                mailMessage.To.Add(email);
            }

            var netwrokCredentials = new NetworkCredential(_SMTPConfig.UserName, _SMTPConfig.Password);

            var smtpClient = new SmtpClient()
            {
                Credentials = netwrokCredentials,
                Port = _SMTPConfig.Port,
                Host = _SMTPConfig.Host,
                UseDefaultCredentials = _SMTPConfig.UseDefaultCredentials,
                EnableSsl = _SMTPConfig.EnableSSL,
            };
            mailMessage.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(mailMessage);
        }
        private string GetEmailBody(string templateName)
        {
            var response = File.ReadAllText(string.Format(templatePath, templateName));
            return response;
        }
        private string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var item in keyValuePairs)
                {
                    if (text.Contains(item.Key))
                    {
                        text = text.Replace(item.Key, item.Value);
                    }
                }
            }
            return text;
        }

        public async Task SendResetPasswordConfirmation(UserEmailOptions userEmailOptions)
        {
            var templateBody = GetEmailBody("ResetPassword");
            userEmailOptions.Body = UpdatePlaceHolders(templateBody, userEmailOptions.PlaceHolders);
            await SendEmail(userEmailOptions);
        }

        public async Task SendAlertQuantityToAdmin(UserEmailOptions userEmailOptions)
        {
            var templateBody = GetEmailBody("AdminAlertQuantity");
            userEmailOptions.Body = UpdatePlaceHolders(templateBody, userEmailOptions.PlaceHolders);
            await SendEmail(userEmailOptions);
        }
    }
}
