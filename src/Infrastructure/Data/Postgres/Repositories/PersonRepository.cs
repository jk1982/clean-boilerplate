using Core.Abstraction.Repositories;
using Core.Models;
using Dapper;
using Infrastructure.Data.Abstraction;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Data.Postgres.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDbSession _dbSession;

        public PersonRepository(IDbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<Guid> Create(Person person)
        {
            var parameters = new DynamicParameters();
            parameters.Add(nameof(person.Name), string.IsNullOrWhiteSpace(person.Name) ? null : person.Name);

            var query = $@"
INSERT INTO person (name)
VALUES (@{nameof(person.Name)})
RETURNING id ";

            Serilog.Log.Information($"Query: { query } ");

            var id = await _dbSession.Connection.QuerySingleAsync<Guid>(query, parameters);

            return id;
        }

    }
}
