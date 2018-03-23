using System;
using System.Collections.Generic;
using System.Linq;
using MiniBlog.Contract;
using MiniBlog.Core;
using MiniBlog.Core.DataAccess.Repositories;
using MiniBlog.Core.DataAccess.UoW;
using MiniBlog.Core.Domain;
using MiniBlog.Core.Mappers;
using Moq;
using NUnit.Framework;
using Serilog;

namespace MiniBlog.Tests
{
    [TestFixture]
    public class BlogTest
    {
        private IRepository<Article> articleRepository;
        private IBlog blog;
        private IRepository<Comment> commentRepository;
        private IRepository<Image> imageRepository;
        private Mock<IUnitOfWork> uowMock;

        [SetUp]
        public void Setup()
        {
            articleRepository = Mock.Of<IRepository<Article>>();
            commentRepository = Mock.Of<IRepository<Comment>>();
            imageRepository = Mock.Of<IRepository<Image>>();

            uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(x => x.RepositoryFor<Article>()).Returns(articleRepository);
            uowMock.Setup(x => x.RepositoryFor<Comment>()).Returns(commentRepository);
            uowMock.Setup(x => x.RepositoryFor<Image>()).Returns(imageRepository);

            var factoryMock = new Mock<IUnitOfWorkFactory>();
            factoryMock.Setup(f => f.Create()).Returns(uowMock.Object);
            var logger = new Mock<ILogger>();
            blog = new Blog(factoryMock.Object, new ObjectMapper(), logger.Object);
        }

        [Test]
        public void AddArticleTest()
        {
            var article = new ArticleDto()
            {
                Id = 42,
                Header = "Head",
                Content = "Content"
            };

            blog.AddArticle(article);

            Mock.Get(articleRepository)
                .Verify(repo => repo.Add(It.IsAny<Article>()), Times.Once);
            Mock.Get(articleRepository).VerifyNoOtherCalls();

            uowMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void AddCommentTest()
        {
            var comment = new CommentDto()
            {
                ArticleId = 123,
                CommentText = "qwerty",
                UserName = "User"
            };

            Mock.Get(articleRepository).Setup(x => x.Get(123)).Returns(new Article() { Id = 123, Comments = new List<Comment>()});

            blog.AddComment(comment);

            Mock.Get(articleRepository).Verify(repo => repo.Get(123), Times.Once);
            Mock.Get(articleRepository).Verify(repo => repo.Update(It.IsAny<Article>()), Times.Once);

            uowMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void DeleteArticleTest()
        {
            Mock.Get(articleRepository).Setup(x => x.Get(54)).Returns(new Article() { Id = 54 });
            blog.DeleteArticle(54);

            Mock.Get(articleRepository).Verify(x => x.Get(It.Is<int>(id => id == 54)), Times.Once);
            Mock.Get(articleRepository).Verify(x => x.Delete(It.Is<Article>(a => a.Id == 54)), Times.Once);
            Mock.Get(articleRepository).VerifyNoOtherCalls();

            uowMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void GetArticleTest()
        {
            var artMock = Mock.Get(articleRepository);
            artMock.Setup(x => x.Get(21)).Returns(new Article() { Image = new Image() { Id = 5 } });

            var article = blog.GetArticle(21);

            artMock.Verify(x => x.Get(It.Is<int>(i => i == 21)), Times.Once);
            artMock.VerifyNoOtherCalls();
        }
    }
}
