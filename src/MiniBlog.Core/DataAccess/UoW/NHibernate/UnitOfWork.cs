using System;
using MiniBlog.Core.DataAccess.Infrastructure.NHibernate;
using MiniBlog.Core.DataAccess.Repositories;
using MiniBlog.Core.DataAccess.Repositories.NHibernate;
using NHibernate;

namespace MiniBlog.Core.DataAccess.UoW.NHibernate
{
    /// <inheritdoc/>
    /// <summary>
    /// Unit of work (NHibernate)
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        private static readonly SessionFactory sessionFactory = new SessionFactory();
        private readonly ISession session;
        private readonly ITransaction transaction;

        private IArticleRepository articleRepository;
        private ICommentRepository commentRepository;
        private bool disposed;
        private IImageRepository imageRepository;

        /// <summary>
        /// C-tor
        /// </summary>
        public UnitOfWork()
        {
            session = sessionFactory.Instance.OpenSession();
            transaction = session.BeginTransaction();
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        /// <inheritdoc/>
        public IArticleRepository ArticleRepository => articleRepository ?? (articleRepository = new ArticleRepository(session));

        /// <inheritdoc/>
        public ICommentRepository CommentRepository => commentRepository ?? (commentRepository = new CommentRepository(session));

        /// <inheritdoc/>
        public IImageRepository ImageRepository => imageRepository ?? (imageRepository = new ImageRepository(session));

        /// <inheritdoc/>
        public void Commit()
        {
            try
            {
                if (transaction.IsActive)
                {
                    transaction.Commit();
                }
            }
            catch
            {
                if (transaction.IsActive)
                {
                    transaction.Rollback();
                }

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
                    session?.Dispose();
                }

                disposed = true;
            }
        }
    }
}
