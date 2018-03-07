using System;
using System.Collections.Generic;
using MiniBlog.Core.DataAccess;
using MiniBlog.Core.Domain;
using NUnit.Framework;

namespace MiniBlog.Tests
{
    [TestFixture]
    [Explicit]
    public class DatabaseTest
    {
        private const string TestDbConnectionString =
            "Server=localhost;Port=5432;Database=blog;User ID=blogUser;Password=blogblog;";

        [Test]
        public void GetArticlePreviewsTest()
        {
            IEnumerable<ArticlePreview> records = OpenDatabase().GetArticlePreviews();

            Assert.IsNotEmpty(records);
        }

        [Test]
        [TestCase(1), TestCase(2), TestCase(3)]
        public void GetArticleTest(int id)
        {
            var record = OpenDatabase().GetArticle(id);
            Assert.NotNull(record);
            Assert.AreEqual(id, record.Id);
        }

        private static Database OpenDatabase() => new Database(new PgConnectionFactory(TestDbConnectionString));
    }
}
