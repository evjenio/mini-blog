using System;
using System.Linq;
using MiniBlog.Core.Domain;
using NHibernate;

namespace MiniBlog.Core.DataAccess.Repositories.NHibernate
{
    public class Repository<TEntity> : Repository, IRepository<TEntity>
        where TEntity : class, IEntity
    {
        public Repository(ISession session)
            : base(session)
        {
        }

        public void Add(TEntity entity)
        {
            Session.Save(entity);
        }

        public void Delete(TEntity entity)
        {
            Session.Delete(entity);
        }

        public TEntity Get(int id)
        {
            return Session.Get<TEntity>(id);
        }

        public IQueryable<TEntity> All()
        {
            return Session.Query<TEntity>();
        }

        public void Update(TEntity entity)
        {
            Session.Update(entity);
        }
    }

    /// <summary>
    /// Abstract repository (NHibernate)
    /// </summary>
    public abstract class Repository
    {
        /// <summary>
        /// C-tor
        /// </summary>
        /// <param name="session">session</param>
        protected Repository(ISession session)
        {
            Session = session;
        }

        /// <summary>
        /// Session
        /// </summary>
        protected ISession Session { get; }
    }
}
