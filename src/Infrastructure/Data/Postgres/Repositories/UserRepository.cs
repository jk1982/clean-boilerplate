using Core.Abstraction.Repositories;
using Core.Models;
using Dapper;
using Infrastructure.Data.Abstraction;
using System.Threading.Tasks;

namespace Infrastructure.Data.Postgres.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbSession _dbSession;

        public UserRepository(IDbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<User> Create(User user)
        {
            var dr = await _dbSession.Connection.ExecuteReaderAsync($@"
INSERT INTO users (email, password_hash)
VALUES (@{nameof(user.Email)}, @{nameof(user.PasswordHash)})
RETURNING id, email, blocked; ", user);

            if (dr.Read())
            {
                return new User((int)dr["id"], (string)dr["email"], "", (bool)dr["blocked"]);
            }

            return null;
        }

        public async Task<bool> Exists(string email)
        {
            var count = await _dbSession.Connection.QuerySingleAsync<int>($@"
SELECT COUNT(id) FROM users
WHERE email = @{nameof(email)} ;", new { email });

            return count > 0;
        }

        public async Task<User> GetBy(string email)
        {
            var user = await _dbSession.Connection.QuerySingleAsync<User>($@"
SELECT id, email, blocked FROM users
WHERE email = @{nameof(email)};", new { email });

            return new User(user.Id, user.Email, "", user.Blocked);
        }

        public async Task<User> GetBy(int id)
        {
            var user = await _dbSession.Connection.QuerySingleAsync<User>($@"
SELECT email, blocked FROM users
WHERE id = @{nameof(id)};", new { id });

            return new User(id, user.Email, "", user.Blocked);
        }

        public async Task<bool> Update(User user)
        {
            var count = await _dbSession.Connection.ExecuteAsync($@"
UPDATE users SET 
email = @{nameof(user.Email)};", new { user.Email });

            return (count > 0);
        }

        public async Task<bool> Validate(string email, string passwordHash)
        {
            var count = await _dbSession.Connection.QuerySingleAsync<int>($@"
SELECT COUNT(id) FROM users
WHERE email = @{nameof(email)} AND password_hash = @{nameof(passwordHash)};", new { email, passwordHash });

            return count > 0;
        }
    }
}
