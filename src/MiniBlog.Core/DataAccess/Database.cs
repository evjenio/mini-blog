using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using Dapper;
using MiniBlog.Core.DataAccess.Model;
using Nelibur.Sword.DataStructures;
using Nelibur.Sword.Extensions;

namespace MiniBlog.Core.DataAccess
{
    /// <summary>
    /// Data access layer.
    /// </summary>
    public class Database
    {
        private const string AddArticleSql =
            "INSERT INTO public.articles (header, content, imageid) " +
            "VALUES (@header, @content, @imageid)";

        private const string AddCommentSql =
            "INSERT INTO public.comments (commenttext, username, articleid) " +
            "VALUES (@commenttext, @username, @article_id)";

        private const string DeleteArticleSql = "DELETE FROM public.articles a WHERE a.id=@article_id";
        private const string InsertImageSql = "INSERT INTO public.images (image) VALUES (@image) RETURNING id";

        private const string SelectAllArticlesSql = "SELECT id, header FROM public.articles ORDER BY id ASC";

        private const string SelectArticleSql =
            "SELECT a.id, a.header, a.content, i.image " +
            "FROM public.articles a " +
            "LEFT OUTER JOIN public.images i " +
            "ON(a.imageid = i.id) WHERE a.id=@article_id;" +
            "SELECT * FROM public.comments c " +
            "WHERE c.articleid=@article_id ORDER BY c.id ASC";

        private readonly IConnectionFactory connectionFactory;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connectionFactory">Factory of connections</param>
        public Database(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        /// <summary>
        /// Adds article.
        /// </summary>
        /// <param name="article">Article object</param>
        public void AddArticle(Article article)
        {
            Contract.Requires(article != null);

            int? InsertImageAndReturnId(byte[] img, IDbConnection connection, IDbTransaction transaction)
            {
                var imageParameters = new DynamicParameters();
                imageParameters.Add("@image", img, DbType.Binary, ParameterDirection.Input);
                return connection.QueryFirst<int>(InsertImageSql, imageParameters, transaction);
            }

            using (var connection = connectionFactory.Create())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    Option<int?> imageId = article.Image.ToOption().Map(img => InsertImageAndReturnId(img, connection, transaction));

                    var parameters = new DynamicParameters();
                    parameters.Add("@header", article.Header, DbType.String, ParameterDirection.Input);
                    parameters.Add("@content", article.Content, DbType.String, ParameterDirection.Input);
                    parameters.Add("@imageid", imageId.Value, DbType.Int32, ParameterDirection.Input);

                    connection.Execute(AddArticleSql, parameters, transaction);

                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Adds comment to article.
        /// </summary>
        /// <param name="articleId">Article identity</param>
        /// <param name="text">Comment text</param>
        /// <param name="userName">User name</param>
        public void AddComment(int articleId, string text, string userName)
        {
            using (var connection = connectionFactory.Create())
            {
                var parameters = CreateParameters(articleId);
                parameters.Add("@commenttext", text, DbType.String, ParameterDirection.Input);
                parameters.Add("@username", userName, DbType.String, ParameterDirection.Input);

                connection.Execute(AddCommentSql, parameters);
            }
        }

        /// <summary>
        /// Deletes article.
        /// </summary>
        /// <param name="articleId">Article identity</param>
        public void DeleteArticle(int articleId)
        {
            using (var connection = connectionFactory.Create())
            {
                var parameters = CreateParameters(articleId);
                connection.Execute(DeleteArticleSql, parameters);
            }
        }

        /// <summary>
        /// Gets article.
        /// </summary>
        /// <param name="articleId">Article identity</param>
        /// <returns>Article object</returns>
        public Article GetArticle(int articleId)
        {
            using (var connection = connectionFactory.Create())
            {
                var parameters = CreateParameters(articleId);

                using (var multi = connection.QueryMultiple(SelectArticleSql, parameters))
                {
                    var article = multi.ReadSingleOrDefault<Article>();
                    List<Comment> comments = multi.Read<Comment>().ToList();
                    return article.ToOption().Do(a => a.Comments = comments).Value;
                }
            }
        }

        /// <summary>
        /// Gets article previews.
        /// </summary>
        /// <returns>List of articles</returns>
        public IEnumerable<ArticlePreview> GetArticlePreviews()
        {
            using (var connection = connectionFactory.Create())
            {
                return connection.Query<ArticlePreview>(SelectAllArticlesSql);
            }
        }

        private static DynamicParameters CreateParameters(int articleId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@article_id", articleId, DbType.Int32, ParameterDirection.Input);
            return parameters;
        }
    }
}
