using Infrastructure.Data.Abstraction;
using Npgsql;
using System.Data;

namespace Infrastructure.Data.Postgres
{
    public sealed class DbSession : IDbSession
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession(string connectionString)
        {
            Connection = new NpgsqlConnection(connectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
