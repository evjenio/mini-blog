using System;
using System.Data;
using MiniBlog.Tests.DataAccess.Repositories.Dapper;
using NUnit.Framework;

namespace MiniBlog.Tests.DataAccess.Repositories
{
    public abstract class RepositoryTestBase<TRepo>
    {
        protected TRepo Repository;
        protected IDbTransaction Transaction;

        [SetUp]
        public void Setup()
        {
            Transaction = TestDb.CreateTransaction();
            Repository = CreateRepository();
        }

        [TearDown]
        public void TearDown()
        {
            Transaction.Rollback();
        }

        protected abstract TRepo CreateRepository();
    }
}
