using System;
using System.Linq;
using MiniBlog.Core.DataAccess.Infrastructure.NHibernate;
using MiniBlog.Core.DataAccess.Repositories.NHibernate;
using MiniBlog.Core.Domain;
using NHibernate;
using NUnit.Framework;

namespace MiniBlog.Tests.DataAccess.Repositories.NHibernate
{
    [TestFixture]
    [Explicit]
    public class RepositoryTest
    {
        private static readonly ISessionFactory sessionFactory;
        static RepositoryTest()
        {
            sessionFactory = SessionFactoryConfigurator.Build();
        }

        [Test]
        public void ScenarioNoImageNoCommentsTest()
        {
            Transaction(session =>
            {
                var articleRepository = new Repository<Article>(session);

                var article = new Article()
                {
                    Content = "Test content",
                    Header = "Heading",
                };

                articleRepository.Add(article);
                session.Flush();

                var dbArticle = articleRepository.Get(article.Id);

                Assert.AreSame(dbArticle, article);
            });
        }

        [Test]
        public void ScenarioWithImageTest()
        {
            int articleId = 0;
            int imageId = 0;
            Transaction(session =>
            {
                var articleRepository = new Repository<Article>(session);

                var article = new Article()
                {
                    Content = "Test content",
                    Header = "Heading",
                };

                articleRepository.Add(article);
                session.Flush();

                var imageRepository = new Repository<Image>(session);
                var image = new Image() { ImageFile = new byte[] { 123 } };
                imageRepository.Add(image);

                article.Image = image;

                articleRepository.Update(article);
                session.Transaction.Commit();
                articleId = article.Id;
                imageId = image.Id;
            });

            Session(session =>
            {
                var article = new Repository<Article>(session).Get(articleId);
                Assert.NotNull(article.Image?.ImageFile);
            });

            Session(session =>
            {
                var articleRepository = new Repository<Article>(session);
                var article = articleRepository.Get(articleId);
                articleRepository.Delete(article);

                var imageRepository = new Repository<Image>(session);
                var image = imageRepository.Get(imageId);
                imageRepository.Delete(image);
                session.Flush();
            });
        }

        [Test]
        public void ScenarioWithCommentsTest()
        {
            int articleId = 0;
            Transaction(session =>
            {
                var articleRepository = new Repository<Article>(session);
                var commentRepository = new Repository<Comment>(session);

                var article = new Article()
                {
                    Content = "Test content",
                    Header = "Heading",
                    Comments = new Comment[] { }
                };
                articleRepository.Add(article);

                var comment = new Comment() { UserName = "Tester", CommentText = "Horrible", Article = article };
                article.Comments.Add(comment);
                commentRepository.Add(comment);

                session.Transaction.Commit();
                session.Flush();
                articleId = article.Id;
            });

            Session(session =>
            {
                var article = new Repository<Article>(session).Get(articleId);
                Assert.AreEqual(1, article.Comments.Count);
                Assert.AreEqual("Tester", article.Comments.Select(x => x.UserName).FirstOrDefault());
            });

            Session(session =>
            {
                var articleRepository = new Repository<Article>(session);
                var article = articleRepository.Get(articleId);
                articleRepository.Delete(article);
            });
        }

        private void Session(Action<ISession> action)
        {
            var session = sessionFactory.OpenSession();

            try
            {
                action(session);
            }
            finally
            {
                session.Dispose();
            }
        }

        private void Transaction(Action<ISession> action)
        {
            var session = sessionFactory.OpenSession();
            var transaction = session.BeginTransaction();

            try
            {
                action(session);
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                session.Dispose();
            }
        }
    }
}
