using System;
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
        private IArticleRepository articleRepository;
        private IBlog blog;
        private ICommentRepository commentRepository;
        private IImageRepository imageRepository;
        private Mock<IUnitOfWork> uowMock;

        [SetUp]
        public void Setup()
        {
            articleRepository = Mock.Of<IArticleRepository>();
            commentRepository = Mock.Of<ICommentRepository>();
            imageRepository = Mock.Of<IImageRepository>();

            uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(x => x.ArticleRepository).Returns(articleRepository);
            uowMock.Setup(x => x.CommentRepository).Returns(commentRepository);
            uowMock.Setup(x => x.ImageRepository).Returns(imageRepository);

            var factoryMock = new Mock<IUnitOfWorkFactory>();
            factoryMock.Setup(f => f.Create()).Returns(uowMock.Object);
            var logger = new Mock<ILogger>();
            blog = new Blog(factoryMock.Object, new ObjectMapper(), logger.Object);
        }

        [Test]
        [TestCase(true, 1)]
        [TestCase(false, 0)]
        public void AddArticleTest(bool withImage, int imagesAddedCount)
        {
            var article = new ArticleDto()
            {
                Id = 42,
                Image = withImage ? new byte[2] : null,
                Header = "Head",
                Content = "Content"
            };

            blog.AddArticle(article);

            Mock.Get(articleRepository)
                .Verify(repo => repo.Add(It.IsAny<Article>()), Times.Once);
            Mock.Get(articleRepository).VerifyNoOtherCalls();

            Mock.Get(imageRepository)
                .Verify(repo => repo.Add(It.IsAny<Image>()), Times.Exactly(imagesAddedCount));
            Mock.Get(imageRepository).VerifyNoOtherCalls();

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

            blog.AddComment(comment);

            Mock.Get(commentRepository)
                .Verify(repo => repo.Add(It.IsAny<Comment>()), Times.Once);
            Mock.Get(commentRepository).VerifyNoOtherCalls();

            uowMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void DeleteArticleTest()
        {
            blog.DeleteArticle(54);

            Mock.Get(articleRepository).Verify(x => x.Delete(It.Is<int>(i => i == 54)), Times.Once);
            Mock.Get(articleRepository).VerifyNoOtherCalls();

            uowMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void GetArticlePreviewsTest()
        {
            ArticlePreviewDto[] articles = blog.GetArticlePreviews();

            Mock.Get(articleRepository).Verify(x => x.GetEntities(), Times.Once);
            Mock.Get(articleRepository).VerifyNoOtherCalls();
        }

        [Test]
        [TestCase(true, 1)]
        [TestCase(false, 0)]
        public void GetArticleTest(bool withImage, int imagesLoadedCount)
        {
            var artMock = Mock.Get(articleRepository);
            artMock.Setup(x => x.Get(21)).Returns(new Article() { ImageId = withImage ? 5 : default(int?) });

            var imgMock = Mock.Get(imageRepository);
            imgMock.Setup(x => x.Get(5)).Returns(new Image());

            var article = blog.GetArticle(21);

            artMock.Verify(x => x.Get(It.Is<int>(i => i == 21)), Times.Once);
            artMock.VerifyNoOtherCalls();

            imgMock.Verify(x => x.Get(It.Is<int>(i => i == 5)), Times.Exactly(imagesLoadedCount));
            imgMock.VerifyNoOtherCalls();
        }
    }
}
