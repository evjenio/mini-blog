using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Npgsql;

namespace MiniBlog.Core.DataAccess
{
    /// <inheritdoc/>
    /// <summary>
    /// Connection factory for PostgreSql.
    /// </summary>
    public class PgConnectionFactory : IConnectionFactory
    {
        private readonly string connectionString;

        private readonly Func<string, DbConnection> factory = conn => new NpgsqlConnection(conn);

        /// <summary>
        /// Constructor.
        /// </summary>
        public PgConnectionFactory()
        {
            connectionString =
                ConfigurationManager.ConnectionStrings["database"].ConnectionString;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public PgConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <inheritdoc/>
        public IDbConnection Create()
        {
            var connection = factory(connectionString);
            connection.Open();
            return connection;
        }

        /// <inheritdoc/>
        public async Task<IDbConnection> CreateAsync()
        {
            var connection = factory(connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
