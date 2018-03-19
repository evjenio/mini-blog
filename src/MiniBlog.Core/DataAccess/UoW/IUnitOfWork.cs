using System;
using MiniBlog.Core.DataAccess.Repositories;

namespace MiniBlog.Core.DataAccess.UoW
{
    /// <inheritdoc />
    /// <summary>
    /// Unit of work
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Article Repository
        /// </summary>
        IArticleRepository ArticleRepository { get; }

        /// <summary>
        /// Comment Repository
        /// </summary>
        ICommentRepository CommentRepository { get; }

        /// <summary>
        /// Image Repository
        /// </summary>
        IImageRepository ImageRepository { get; }

        /// <summary>
        /// Commit changes
        /// </summary>
        void Commit();
    }
}
