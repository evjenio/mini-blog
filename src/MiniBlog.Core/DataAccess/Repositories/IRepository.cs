using System;
using System.Collections.Generic;
using System.Linq;
using MiniBlog.Core.Domain;

namespace MiniBlog.Core.DataAccess.Repositories
{
    /// <summary>
    /// Generic repository.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TIdentity">Identity type</typeparam>
    public interface IRepository<TEntity, in TIdentity>
        where TIdentity : struct
        where TEntity : IEntity<TIdentity>
    {
        /// <summary>
        /// Adds entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        void Add(TEntity entity);

        /// <summary>
        /// Removes entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Gets entity.
        /// </summary>
        /// <param name="id">Entity identity</param>
        /// <returns>Entity</returns>
        TEntity Get(TIdentity id);

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>List of entities</returns>
        IQueryable<TEntity> All();

        /// <summary>
        /// Updates entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(TEntity entity);
    }

    public interface IRepository<TEntity> : IRepository<TEntity, int>
        where TEntity : IEntity
    {
    }
}
