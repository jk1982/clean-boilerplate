using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services.Email
{
    public class EmailOptions
    {
        public const string SectionName = "Email";

        public string FromAddress { get; set; }
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
        public string UrlConfirmationEmail { get; set; }
    }
}
