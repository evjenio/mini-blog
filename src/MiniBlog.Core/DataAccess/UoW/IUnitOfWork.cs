using System;
using MiniBlog.Core.DataAccess.Repositories;
using MiniBlog.Core.Domain;

namespace MiniBlog.Core.DataAccess.UoW
{
    /// <inheritdoc />
    /// <summary>
    /// Unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Repository for entity.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>Entity</returns>
        IRepository<TEntity> RepositoryFor<TEntity>() where TEntity : IEntity;

        /// <summary>
        /// Commit changes.
        /// </summary>
        void Commit();
    }
}
