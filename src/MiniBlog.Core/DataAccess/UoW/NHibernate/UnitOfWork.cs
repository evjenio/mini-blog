using System;
using MiniBlog.Core.DataAccess.Repositories;
using MiniBlog.Core.Domain;
using NHibernate;

namespace MiniBlog.Core.DataAccess.UoW.NHibernate
{
    /// <inheritdoc/>
    /// <summary>
    /// Unit of work (NHibernate)
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly IRepositoryResolver repositoryResolver;
        private readonly ITransaction transaction;
        private bool disposed;

        /// <summary>
        /// C-tor
        /// </summary>
        public UnitOfWork(ISession session, IRepositoryResolver repositoryResolver)
        {
            this.repositoryResolver = repositoryResolver;
            transaction = session.BeginTransaction();
        }

        public IRepository<TEntity> RepositoryFor<TEntity>()
            where TEntity : IEntity
        {
            return repositoryResolver.ResolveFor<TEntity>();
        }

        /// <inheritdoc/>
        public void Commit()
        {
            try
            {
                if (transaction.IsActive)
                {
                    transaction.Commit();
                }
            }
            catch
            {
                if (transaction.IsActive)
                {
                    transaction.Rollback();
                }

                throw;
            }
            finally
            {
                transaction.Dispose();
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (!disposed)
            {
                transaction?.Dispose();
                disposed = true;
            }
        }
    }
}
