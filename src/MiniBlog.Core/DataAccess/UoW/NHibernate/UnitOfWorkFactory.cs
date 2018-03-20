using System;
using MiniBlog.Core.DataAccess.Repositories;
using NHibernate;

namespace MiniBlog.Core.DataAccess.UoW.NHibernate
{
    /// <inheritdoc/>
    /// <summary>
    /// Unit Of Work Factory (NHibernate)
    /// </summary>
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IRepositoryResolver repositoryResolver;
        private readonly ISession session;

        /// <summary>
        /// C-tor
        /// </summary>
        /// <param name="session">Session</param>
        /// <param name="repositoryResolver"></param>
        public UnitOfWorkFactory(ISession session, IRepositoryResolver repositoryResolver)
        {
            this.session = session;
            this.repositoryResolver = repositoryResolver;
        }

        /// <inheritdoc/>
        public IUnitOfWork Create() => new UnitOfWork(session, repositoryResolver);
    }
}
