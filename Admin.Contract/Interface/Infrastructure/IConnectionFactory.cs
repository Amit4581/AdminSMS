using System.Data;

namespace Admin.Contract.Interface.Infrastructure
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }

    }
}
