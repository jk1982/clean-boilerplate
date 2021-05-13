using Core.Abstraction.Services;
using Core.DTO.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Jwt
{
    public class JwtFactory : IJwtFactory
    {
        private readonly IOptions<TokenSettings> _jwtSettings;

        public JwtFactory(IOptions<TokenSettings> jwtSettings)
        {
            this._jwtSettings = jwtSettings;
        }

        public Task<Token> GenerateAuthToken(int id, string email)
        {
            var expires = DateTime.UtcNow.AddHours(this._jwtSettings.Value.Jwt.ExpirationHours);
            var token = Generate(this._jwtSettings.Value.Jwt.Secret, expires, email);

            return Task.FromResult(new Token(id, token, new DateTimeOffset(expires).ToUnixTimeSeconds()));
        }

        public Task<string> GenerateEmailConfirmationToken(string email)
        {
            var expires = DateTime.UtcNow.AddHours(this._jwtSettings.Value.Email.ExpirationHours);
            var token = Generate(this._jwtSettings.Value.Email.Secret, expires, email);

            return Task.FromResult(token);
        }

        public Task<bool> VerifyEmailConfirmationToken(string token)
        {
            throw new NotImplementedException();
        }

        private string Generate(string secret, DateTime expires, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Expiration, expires.ToString())
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        private bool Verify(string secret, string token)
        {
            //try
            //{
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            //}
            //catch (SecurityTokenDecryptionFailedException)
            //{ 
            //}
            //catch (SecurityTokenExpiredException)
            //{
            //}
            //catch (SecurityTokenInvalidSignatureException)
            //{
            //}

            return true;
        }
    }
}
