using System;
using System.Collections.Generic;
using MiniBlog.Core.DataAccess.Repositories;
using MiniBlog.Core.DataAccess.Repositories.Dapper;
using MiniBlog.Core.Domain;
using NUnit.Framework;

namespace MiniBlog.Tests.DataAccess.Repositories.Dapper
{
    [TestFixture, Explicit]
    public class ImageRepositoryTest : RepositoryTestBase<IImageRepository>
    {
        [Test]
        public void GetEntitiesTest()
        {
            IEnumerable<Image> records = Repository.GetEntities();

            Assert.IsNotEmpty(records);
        }

        protected override IImageRepository CreateRepository() => new ImageRepository(Transaction);
    }
}
