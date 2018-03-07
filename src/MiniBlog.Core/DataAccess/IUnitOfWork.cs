using System;
using MiniBlog.Core.DataAccess.Repositories;

namespace MiniBlog.Core.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IArticleRepository ArticleRepository { get; }
        ICommentRepository CommentRepository { get; }
        IImageRepository ImageRepository { get; }

        void Commit();
    }
}
