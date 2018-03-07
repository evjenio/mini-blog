using MiniBlog.Core.Domain;

namespace MiniBlog.Core.DataAccess.Repositories
{
    public interface IArticleRepository : IRepository<Article, int>
    {
    }
}
