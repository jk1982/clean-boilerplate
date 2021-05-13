using Core.DTO.Jwt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO.Responses.Auth
{
    public class LoginResponse : ServiceResponse
    {
        public LoginResponse(Token token) : base(true)
        {
            Token = token;
        }

        public LoginResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(errors, success, message)
        {
        }

        public Token Token { get; }
    }
}
