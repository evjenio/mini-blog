using System;
using System.Collections.Generic;
using System.Linq;
using MiniBlog.Core.Domain;
using NHibernate;

namespace MiniBlog.Core.DataAccess.Repositories.NHibernate
{
    /// <summary>
    /// Comment Repository
    /// </summary>
    public class CommentRepository : Repository, ICommentRepository
    {
        /// <summary>
        /// C-tor
        /// </summary>
        /// <param name="session">Session</param>
        public CommentRepository(ISession session)
            : base(session)
        {
        }

        /// <inheritdoc/>
        public int Add(Comment entity)
        {
            Session.Save(entity);
            return entity.Id;
        }

        /// <inheritdoc/>
        public void Delete(Comment entity)
        {
            Session.Delete(entity);
        }

        /// <inheritdoc/>
        public void Delete(int id)
        {
            Session.Delete(new Comment() { Id = id });
        }

        /// <inheritdoc/>
        public Comment Get(int id)
        {
            return Session.Get<Comment>(id);
        }

        /// <inheritdoc/>
        public IEnumerable<Comment> GetCommentsForArticle(int articleId)
        {
            return Session.QueryOver<Comment>()
                .Where(c => c.ArticleId == articleId)
                .OrderBy(i => i.Id).Asc
                .List();
        }

        /// <inheritdoc/>
        public IEnumerable<Comment> GetEntities()
        {
            return Session.Query<Comment>().ToList();
        }

        /// <inheritdoc/>
        public void Update(Comment entity)
        {
            Session.Update(entity);
        }
    }
}
