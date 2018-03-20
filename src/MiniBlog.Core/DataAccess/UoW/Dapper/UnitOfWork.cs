using System;
using System.Data;
using MiniBlog.Core.DataAccess.Repositories;
using MiniBlog.Core.Domain;

namespace MiniBlog.Core.DataAccess.UoW.Dapper
{
    /// <inheritdoc/>
    /// <summary>
    /// Unit of work (Dapper)
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;
        private bool disposed;

        /// <summary>
        /// C-tor
        /// </summary>
        /// <param name="connectionFactory">Connection factory</param>
        public UnitOfWork(IConnectionFactory connectionFactory)
        {
            connection = connectionFactory.Create();
            transaction = connection.BeginTransaction();
        }

        public IRepository<TEntity> RepositoryFor<TEntity>()
            where TEntity : IEntity
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Commit()
        {
            try
            {
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                transaction.Dispose();
            }
        }


        public void Dispose()
        {
            if (!disposed)
            {
                transaction?.Dispose();
                connection?.Dispose();
                disposed = true;
            }
        }
    }
}
