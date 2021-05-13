using System;
using System.Data;

namespace Infrastructure.Data.Abstraction
{
    public interface IDbSession : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; set; }
    }
}
