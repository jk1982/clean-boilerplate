using Core.Abstraction.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Email
{
    public class UserEmailManager : IUserEmailManager
    {
        private readonly IEmailSender _emailSender;
        private readonly IJwtFactory _jwtFactory;
        private readonly IOptions<EmailOptions> _options;

        public UserEmailManager(IEmailSender emailSender, IJwtFactory jwtFactory, IOptions<EmailOptions> options)
        {
            _emailSender = emailSender;
            _jwtFactory = jwtFactory;
            _options = options;
        }

        public async Task SendConfirmationEmail(int id, string email)
        {
            var validEmailToken = await _jwtFactory.GenerateEmailConfirmationToken(email);

            validEmailToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(validEmailToken));

            string url = $"{_options.Value.UrlConfirmationEmail}?userId={id}&token={validEmailToken}";

            await _emailSender.SendEmailAsync(email, "Please Confirm your email", $"<h2>Welcome to Empreenda.Me Demo</h2>" +
                    $"<p>Please confirm your email by <a href='{url}'>clicking here</a></p>");
        }

        public Task<bool> ConfirmEmail(int id, string token)
        {
            throw new NotImplementedException();
        }
    }
}
