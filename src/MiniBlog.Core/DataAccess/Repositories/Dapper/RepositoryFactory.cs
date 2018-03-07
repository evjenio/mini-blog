using System;
using System.Data;

namespace MiniBlog.Core.DataAccess.Repositories.Dapper
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IArticleRepository CreateArticleRepository(IDbTransaction transaction)
        {
            return new ArticleRepository(transaction);
        }

        public ICommentRepository CreateCommentRepository(IDbTransaction transaction)
        {
            return new CommentRepository(transaction);
        }

        public IImageRepository CreateImageRepository(IDbTransaction transaction)
        {
            return new ImageRepository(transaction);
        }
    }
}
