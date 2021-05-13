using Core.Abstraction.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailOptions _options;

        public EmailSender(IOptions<EmailOptions> options)
        {
            this._options = options.Value;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(_options.SendGridKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_options.FromAddress, _options.SendGridUser),
                Subject = subject,
                PlainTextContent = htmlMessage,
                HtmlContent = htmlMessage
            };

            msg.AddTo(new EmailAddress(email));
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}
