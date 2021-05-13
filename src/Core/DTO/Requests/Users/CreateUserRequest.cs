using Core.Abstraction;
using Core.DTO.Responses.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO.Requests.Users
{
    public class CreateUserRequest : IUseCaseRequest<UserResponse>
    {
        public CreateUserRequest(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }

        public string Email { get; }
        public string PasswordHash { get; }
    }
}
