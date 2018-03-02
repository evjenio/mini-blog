using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Npgsql;

namespace MiniBlog.Core.DataAccess
{
    public class PgConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;

        private readonly Func<string, DbConnection> _factory = conn => new NpgsqlConnection(conn);

        public PgConnectionFactory()
        {
            _connectionString =
                ConfigurationManager.ConnectionStrings["database"].ConnectionString;
        }

        public PgConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection Create()
        {
            var connection = _factory(_connectionString);
            connection.Open();
            return connection;
        }

        public async Task<IDbConnection> CreateAsync()
        {
            var connection = _factory(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}