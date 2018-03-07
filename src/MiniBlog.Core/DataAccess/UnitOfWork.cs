using System;
using System.Data;
using MiniBlog.Core.DataAccess.Repositories;

namespace MiniBlog.Core.DataAccess
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly IRepositoryFactory repositoryFactory;
        private IArticleRepository articleRepository;
        private ICommentRepository commentRepository;
        private IDbConnection connection;
        private bool disposed;
        private IImageRepository imageRepository;
        private IDbTransaction transaction;

        public UnitOfWork(IConnectionFactory connectionFactory, IRepositoryFactory repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory;
            connection = connectionFactory.Create();
            transaction = connection.BeginTransaction();
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        public IArticleRepository ArticleRepository => articleRepository ?? (articleRepository = repositoryFactory.CreateArticleRepository(transaction));
        public ICommentRepository CommentRepository => commentRepository ?? (commentRepository = repositoryFactory.CreateCommentRepository(transaction));
        public IImageRepository ImageRepository => imageRepository ?? (imageRepository = repositoryFactory.CreateImageRepository(transaction));

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
                transaction = connection.BeginTransaction();
                ResetRepositories();
            }
        }

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
                    if (transaction != null)
                    {
                        transaction.Dispose();
                        transaction = null;
                    }

                    if (connection != null)
                    {
                        connection.Dispose();
                        connection = null;
                    }
                }

                disposed = true;
            }
        }

        private void ResetRepositories()
        {
            articleRepository = null;
            commentRepository = null;
            imageRepository = null;
        }
    }
}
