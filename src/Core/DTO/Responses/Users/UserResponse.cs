using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;

namespace Core.DTO.Responses.Users
{
    public class UserResponse : ServiceResponse
    {
        public UserResponse(User user) : base(true, "")
        {
            User = user;
        }

        public UserResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(errors, success, message)
        {
        }

        public User User { get; }
    }
}
