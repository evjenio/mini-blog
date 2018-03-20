using System;
using Autofac;
using MiniBlog.Core.DataAccess.Repositories;
using MiniBlog.Core.Domain;

namespace MiniBlog.WCF.Infrastructure
{
    public class RepositoryResolver : IRepositoryResolver
    {
        private readonly IComponentContext componentContext;

        public RepositoryResolver(IComponentContext componentContext)
        {
            this.componentContext = componentContext;
        }

        public IRepository<TEntity> ResolveFor<TEntity>()
            where TEntity : IEntity
        {
            return componentContext.Resolve<IRepository<TEntity>>();
        }
    }
}
