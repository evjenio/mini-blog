using System;
using System.Data;
using MiniBlog.Core.DataAccess.Repositories;
using MiniBlog.Core.DataAccess.Repositories.Dapper;

namespace MiniBlog.Core.DataAccess.UoW.Dapper
{
    /// <inheritdoc/>
    /// <summary>
    /// Unit of work (Dapper)
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;
        private IArticleRepository articleRepository;
        private ICommentRepository commentRepository;
        private bool disposed;
        private IImageRepository imageRepository;

        /// <summary>
        /// C-tor
        /// </summary>
        /// <param name="connectionFactory">Connection factory</param>
        public UnitOfWork(IConnectionFactory connectionFactory)
        {
            connection = connectionFactory.Create();
            transaction = connection.BeginTransaction();
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        /// <inheritdoc/>
        public IArticleRepository ArticleRepository => articleRepository ?? (articleRepository = new ArticleRepository(transaction));

        /// <inheritdoc/>
        public ICommentRepository CommentRepository => commentRepository ?? (commentRepository = new CommentRepository(transaction));

        /// <inheritdoc/>
        public IImageRepository ImageRepository => imageRepository ?? (imageRepository = new ImageRepository(transaction));

        /// <inheritdoc/>
        public void Commit()
        {
            try
            {
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                transaction.Dispose();
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    transaction?.Dispose();
                    connection?.Dispose();
                }

                disposed = true;
            }
        }
    }
}
