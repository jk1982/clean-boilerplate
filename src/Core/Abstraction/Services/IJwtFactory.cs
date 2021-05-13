using Core.DTO;
using Core.DTO.Jwt;
using System.Threading.Tasks;

namespace Core.Abstraction.Services
{
    public interface IJwtFactory
    {
        Task<Token> GenerateAuthToken(int id, string email);
        Task<string> GenerateEmailConfirmationToken(string email);
        Task<bool> VerifyEmailConfirmationToken(string token);
    }
}
