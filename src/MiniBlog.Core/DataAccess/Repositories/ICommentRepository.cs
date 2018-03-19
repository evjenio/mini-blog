using System.Collections.Generic;
using MiniBlog.Core.Domain;

namespace MiniBlog.Core.DataAccess.Repositories
{
    /// <summary>
    /// Comment Repository
    /// </summary>
    public interface ICommentRepository : IRepository<Comment, int>
    {
        IEnumerable<Comment> GetCommentsForArticle(int articleId);
    }
}
