using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services.Jwt
{
    public class TokenSettings
    {
        public const string SectionName = "TokenSettings";
        public JwtSettings Jwt { get; set; }
        public EmailSettings Email { get; set; }       
        
    }

    public class JwtSettings
    {
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
    }

    public class EmailSettings
    {
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
    }
}
