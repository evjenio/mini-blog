using System;
using System.Collections.Generic;
using MiniBlog.Core.DataAccess.Repositories;
using MiniBlog.Core.DataAccess.Repositories.Dapper;
using MiniBlog.Core.Domain;
using NUnit.Framework;

namespace MiniBlog.Tests.DataAccess.Repositories.Dapper
{
    [TestFixture]
    [Explicit]
    public class CommentRepositoryTest : RepositoryTestBase<ICommentRepository>
    {
        [Test]
        public void AddTest()
        {
            var comment = new Comment()
            {
                UserName = "User",
                CommentText = "Text",
                ArticleId = 1
            };
            Repository.Add(comment);

            Assert.AreNotEqual(0, comment.Id);
        }

        [Test]
        public void AddTest_Throws()
        {
            var comment = new Comment()
            {
                UserName = "User",
                CommentText = "Text",
                ArticleId = 666
            };
            var ex = Assert.Catch(() => Repository.Add(comment));
            Assert.That(ex.Message.Contains("articleId_fk"));
        }

        [Test]
        [TestCase(2)]
        public void GetCommentsForArticleTest(int id)
        {
            IEnumerable<Comment> records = Repository.GetCommentsForArticle(id);
            Assert.IsNotEmpty(records);
        }

        [Test]
        public void GetEntitiesTest()
        {
            IEnumerable<Comment> records = Repository.GetEntities();

            Assert.IsNotEmpty(records);
        }

        [Test]
        [TestCase(4), TestCase(5), TestCase(6)]
        public void GetTest(int id)
        {
            var record = Repository.Get(id);
            Assert.NotNull(record);
            Assert.AreEqual(id, record.Id);
        }

        protected override ICommentRepository CreateRepository() => new CommentRepository(Transaction);
    }
}
