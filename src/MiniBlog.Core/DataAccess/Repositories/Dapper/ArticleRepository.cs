using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using MiniBlog.Core.Domain;

namespace MiniBlog.Core.DataAccess.Repositories.Dapper
{
    /// <summary>
    /// Article Repository
    /// </summary>
    public class ArticleRepository : Repository, IArticleRepository
    {
        /// <summary>
        /// C-tor
        /// </summary>
        /// <param name="transaction">Transaction</param>
        public ArticleRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        /// <inheritdoc/>
        public int Add(Article entity)
        {
            const string sql = @"INSERT INTO public.articles (header, content, imageid) 
                                            VALUES (@header, @content, @imageid) 
                                            RETURNING id";
            var parameters = new { header = entity.Header, content = entity.Content, imageid = entity.ImageId };
            var id = Connection.QueryFirst<int>(sql, parameters, Transaction);
            entity.Id = id;
            return id;
        }

        /// <inheritdoc/>
        public void Delete(Article entity)
        {
            Connection.Execute("DELETE FROM public.articles a WHERE a.id = @id", new { id = entity.Id }, Transaction);
        }

        /// <inheritdoc/>
        public void Delete(int id)
        {
            Connection.Execute("DELETE FROM public.articles a WHERE a.id = @id", new { id }, Transaction);
        }

        /// <inheritdoc/>
        public Article Get(int id)
        {
            return Connection.QuerySingleOrDefault<Article>("SELECT * FROM public.articles WHERE id = @id", new { id }, Transaction);
        }

        /// <inheritdoc/>
        public IEnumerable<Article> GetEntities()
        {
            return Connection.Query<Article>("SELECT id, header FROM public.articles ORDER BY id ASC");
        }

        /// <inheritdoc/>
        public void Update(Article entity)
        {
            // TODO: implement
            throw new NotImplementedException();
        }
    }
}
