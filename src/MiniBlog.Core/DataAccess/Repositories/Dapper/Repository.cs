using System;
using System.Data;

namespace MiniBlog.Core.DataAccess.Repositories.Dapper
{
    /// <summary>
    /// Abstract repository
    /// </summary>
    public abstract class Repository
    {
        /// <summary>
        /// C-tor
        /// </summary>
        /// <param name="transaction">Transaction</param>
        protected Repository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }

        /// <summary>
        /// Connection
        /// </summary>
        protected IDbConnection Connection => Transaction.Connection;

        /// <summary>
        /// Transaction
        /// </summary>
        protected IDbTransaction Transaction { get; }
    }
}
