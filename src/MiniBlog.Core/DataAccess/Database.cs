using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MiniBlog.Core.Model;
using Nelibur.Sword.Extensions;

namespace MiniBlog.Core.DataAccess
{
    public class Database
    {
        private const string SelectAllArticlesSql = "SELECT id, header FROM public.articles ORDER BY id ASC";

        private const string SelectArticleSql =
            "SELECT a.id, a.header, a.content, i.image " +
            "FROM public.articles a " +
            "LEFT OUTER JOIN public.images i " +
            "ON(a.imageid = i.id) WHERE a.id=@article_id;" +
            "SELECT * FROM public.comments c " +
            "WHERE c.articleid=@article_id ORDER BY c.id ASC";

        private readonly IConnectionFactory _connectionFactory;

        public Database(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Article GetArticle(int articleId)
        {
            using (var connection = _connectionFactory.Create())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@article_id", articleId, DbType.Int32, ParameterDirection.Input);

                using (var multi = connection.QueryMultiple(SelectArticleSql, parameters))
                {
                    var article = multi.ReadSingleOrDefault<Article>();
                    var comments = multi.Read<Comment>().ToList();
                    return article.ToOption().Do(a => a.Comments = comments).Value;
                }
            }
        }

        public IEnumerable<ArticlePreview> GetArticlePreviews()
        {
            using (var connection = _connectionFactory.Create())
            {
                return connection.Query<ArticlePreview>(SelectAllArticlesSql).ToList();
            }
        }
    }
}