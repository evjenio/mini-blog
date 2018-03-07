using System.Data;

namespace MiniBlog.Core.DataAccess.Repositories
{
    public interface IRepositoryFactory
    {
        IArticleRepository CreateArticleRepository(IDbTransaction transaction);
        ICommentRepository CreateCommentRepository(IDbTransaction transaction);
        IImageRepository CreateImageRepository(IDbTransaction transaction);
    }
}
