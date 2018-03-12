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
    public class ArticleRepositoryTest : RepositoryTestBase<IArticleRepository>
    {
        [Test]
        public void AddTest()
        {
            var article = new Article()
            {
                Content = "TestContent",
                Header = "TestHeader",
            };
            Repository.Add(article);

            Assert.AreNotEqual(0, article.Id);
        }

        [Test]
        public void GetEntitiesTest()
        {
            IEnumerable<Article> records = Repository.GetEntities();

            Assert.IsNotEmpty(records);
        }

        [Test]
        [TestCase(1), TestCase(2), TestCase(3)]
        public void GetTest(int id)
        {
            var record = Repository.Get(id);
            Assert.NotNull(record);
            Assert.AreEqual(id, record.Id);
        }

        protected override IArticleRepository CreateRepository() => new ArticleRepository(Transaction);
    }
}
