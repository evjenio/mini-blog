using System;
using System.Data;
using MiniBlog.Core.DataAccess;

namespace MiniBlog.Tests.DataAccess.Repositories.Dapper
{
    /// <summary>
    /// 
    /// </summary>
    internal static class TestDb
    {
        private const string ConnectionString =
            "Server=localhost;Port=5432;Database=blog;User ID=blogUser;Password=blogblog;";

        public static IDbTransaction CreateTransaction()
        {
            var connectionFactory = new PgConnectionFactory(ConnectionString);
            return connectionFactory.Create().BeginTransaction();
        }
    }
}
