using System;
using System.Data;
using System.Threading.Tasks;

namespace MiniBlog.Core.DataAccess
{
    /// <summary>
    /// Factory of connections.
    /// </summary>
    public interface IConnectionFactory
    {
        /// <summary>
        /// Creates Db connection
        /// </summary>
        /// <returns>Db connection</returns>
        IDbConnection Create();

        /// <summary>
        /// Creates Db connection
        /// </summary>
        /// <returns>Db connection</returns>
        Task<IDbConnection> CreateAsync();
    }
}
