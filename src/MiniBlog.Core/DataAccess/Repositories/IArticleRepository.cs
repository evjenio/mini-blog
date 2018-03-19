using MiniBlog.Core.Domain;

namespace MiniBlog.Core.DataAccess.Repositories
{
    /// <summary>
    /// Article Repository
    /// </summary>
    public interface IArticleRepository : IRepository<Article, int>
    {
    }
}
