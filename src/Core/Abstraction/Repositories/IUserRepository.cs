using Core.DTO.Responses.GatewayResponses.Repositories;
using Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Abstraction.Repositories
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<bool> Update(User user);
        Task<bool> Exists(string email);
        Task<User> GetBy(int id);
        Task<User> GetBy(string email);
        Task<bool> Validate(string email, string passwordHash);
    }
}