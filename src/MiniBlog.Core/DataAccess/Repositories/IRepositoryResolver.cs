using MiniBlog.Core.Domain;

namespace MiniBlog.Core.DataAccess.Repositories
{
    public interface IRepositoryResolver
    {
        IRepository<TEntity> ResolveFor<TEntity>()
            where TEntity : IEntity;
    }
}
