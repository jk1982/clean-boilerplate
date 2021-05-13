using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstraction.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
