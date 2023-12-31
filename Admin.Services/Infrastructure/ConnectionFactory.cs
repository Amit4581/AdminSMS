

using System.Data.Common;
using System.Data;
using Admin.Contract.Interface.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Admin.Services.Services.Infrastructure
{

    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;

        public ConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConnection");

            // Register the SQL Server provider factory during application startup
            RegisterSqlClientFactory();
        }

        private static void RegisterSqlClientFactory()
        {
            // Check if the factory is already registered
            if (DbProviderFactories.GetFactoryClasses().Rows.Contains("System.Data.SqlClient"))
            {
                return;
            }

            // Register the SQL Server provider factory if not already registered
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", Microsoft.Data.SqlClient.SqlClientFactory.Instance);
        }

        public IDbConnection GetConnection
        {
            get
            {
                var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                var conn = factory.CreateConnection();
                conn.ConnectionString = _connectionString;
                return conn;
            }
        }
    }


    //public class ConnectionFactory : IConnectionFactory
    //{
    //    private readonly IConfiguration _configuration;
    //    private readonly string _connectionString;
    //    public ConnectionFactory(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //        _connectionString = _configuration.GetConnectionString("SqlConnection");
    //    }
    //    public IDbConnection GetConnection
    //    {
    //        get
    //        {
    //            DbProviderFactories.RegisterFactory("System.Data.SqlClient", Microsoft.Data.SqlClient.SqlClientFactory.Instance);
    //            var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
    //            var conn = factory.CreateConnection();
    //            conn.ConnectionString = _connectionString;
    //            return conn;
    //        }
    //    }
    //}
}
