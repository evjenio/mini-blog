using System;
using System.Collections.Generic;
using MiniBlog.Core.Domain;

namespace MiniBlog.Core.DataAccess.Repositories
{
    /// <summary>
    /// Generic repository.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TIdentity">Identity type</typeparam>
    public interface IRepository<TEntity, TIdentity>
        where TIdentity : struct
        where TEntity : IEntity<TIdentity>
    {
        /// <summary>
        /// Adds entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        TIdentity Add(TEntity entity);

        /// <summary>
        /// Removes entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Removes entity.
        /// </summary>
        /// <param name="id">Entity identity</param>
        void Delete(TIdentity id);

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
        IEnumerable<TEntity> GetEntities();

        /// <summary>
        /// Updates entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(TEntity entity);
    }
}
