using System;
using System.Collections.Generic;
using System.Data;
using MiniBlog.Core.Domain;

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
