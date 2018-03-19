using System;
using System.Collections.Generic;
using MiniBlog.Core.Domain;
using NHibernate;
using NHibernate.Transform;

namespace MiniBlog.Core.DataAccess.Repositories.NHibernate
{
    /// <summary>
    /// Article Repository
    /// </summary>
    public class ArticleRepository : Repository, IArticleRepository
    {
        /// <summary>
        /// C-tor
        /// </summary>
        /// <param name="session">Session</param>
        public ArticleRepository(ISession session)
            : base(session)
        {
        }

        /// <inheritdoc/>
        public int Add(Article entity)
        {
            Session.Save(entity);
            return entity.Id;
        }

        /// <inheritdoc/>
        public void Delete(Article entity)
        {
            Session.Delete(entity);
        }

        /// <inheritdoc/>
        public void Delete(int id)
        {
            Session.Delete(new Article() { Id = id });
        }

        /// <inheritdoc/>
        public Article Get(int id)
        {
            return Session.Get<Article>(id);
        }

        /// <inheritdoc/>
        public IEnumerable<Article> GetEntities()
        {
            // all columns except "Content"
            Article article = null;
            return Session.QueryOver<Article>()
                .SelectList(list => list
                    .Select(p => p.Id).WithAlias(() => article.Id)
                    .Select(p => p.Header).WithAlias(() => article.Header)
                    .Select(p => p.ImageId).WithAlias(() => article.ImageId))
                .TransformUsing(Transformers.AliasToBean<Article>())
                .List<Article>();
        }

        /// <inheritdoc/>
        public void Update(Article entity)
        {
            Session.Update(entity);
        }
    }
}
