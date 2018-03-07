using System.Collections.Generic;
using MiniBlog.Core.Domain;

namespace MiniBlog.Core.DataAccess.Repositories
{
    public interface ICommentRepository : IRepository<Comment, int>
    {
        IEnumerable<Comment> GetCommentsForArticle(int articleId);
    }
}
