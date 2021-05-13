using Core.DTO.Responses.GatewayResponses.Repositories;
using Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Abstraction.Repositories
{
    public interface IPersonRepository
    {
        Task<Guid> Create(Person person);
    }
}
