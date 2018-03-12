using System;
using System.Data;

namespace MiniBlog.Core.DataAccess.Repositories
{
    public abstract class Repository
    {
        protected IDbTransaction Transaction { get; }
        protected IDbConnection Connection => Transaction.Connection;

        protected Repository(IDbTransaction transaction)
        {
            this.Transaction = transaction;
        }
    }
}
