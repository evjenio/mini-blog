using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using MiniBlog.Core.Domain;

namespace MiniBlog.Core.DataAccess.Repositories.Dapper
{
    /// <summary>
    /// Comment repository
    /// </summary>
    public class CommentRepository : Repository, ICommentRepository
    {
        /// <summary>
        /// C-tor
        /// </summary>
        /// <param name="transaction">Transaction</param>
        public CommentRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        /// <inheritdoc/>
        public int Add(Comment entity)
        {
            const string sql =
                @"INSERT INTO public.comments (commenttext, username, articleid) 
                VALUES (@commenttext, @username, @articleid) 
                RETURNING id";

            var parameters = new
            {
                articleId = entity.ArticleId,
                commenttext = entity.CommentText,
                username = entity.UserName
            };

            var id = Connection.QueryFirst<int>(sql, parameters, Transaction);
            entity.Id = id;
            return id;
        }

        /// <inheritdoc/>
        public void Delete(Comment entity)
        {
            Connection.Execute("DELETE FROM public.comments WHERE id = @id", new { id = entity.Id }, Transaction);
        }

        /// <inheritdoc/>
        public void Delete(int id)
        {
            Connection.Execute("DELETE FROM public.comments WHERE id = @id", new { id }, Transaction);
        }

        /// <inheritdoc/>
        public Comment Get(int id)
        {
            return Connection.QuerySingleOrDefault<Comment>("SELECT * FROM public.comments WHERE id = @id", new { id }, Transaction);
        }

        /// <inheritdoc/>
        public IEnumerable<Comment> GetCommentsForArticle(int articleId)
        {
            const string sql = @"SELECT * FROM public.comments 
                                WHERE articleid = @articleid ORDER BY id ASC";
            return Connection.Query<Comment>(sql, new { articleid = articleId }, Transaction);
        }

        /// <inheritdoc/>
        public IEnumerable<Comment> GetEntities()
        {
            return Connection.Query<Comment>("SELECT * FROM public.comments ORDER BY id ASC", Transaction);
        }

        /// <inheritdoc/>
        public void Update(Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}
