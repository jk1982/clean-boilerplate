using Core.Abstraction;
using Core.DTO.Responses.Auth;

namespace Core.DTO.Requests.Auth
{
    public class LoginRequest : IUseCaseRequest<LoginResponse>
    {
        public LoginRequest(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }

        public string Email { get; }
        public string PasswordHash { get; }
    }
}
