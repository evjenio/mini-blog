using System;
using System.Diagnostics.Contracts;
using System.Linq;
using MiniBlog.Core.DataAccess;
using MiniBlog.DataContract;
using MiniBlog.WCF.ErrorHandling;
using Serilog;

namespace MiniBlog.WCF.Services
{
    /// <summary>
    /// Blog Service.
    /// </summary>
    [GlobalErrorBehavior(typeof(GlobalErrorHandler))]
    public class BlogService : IBlogService
    {
        private readonly Database database;
        private readonly ILogger logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="database">DB</param>
        /// <param name="logger">Logger</param>
        public BlogService(Database database, ILogger logger)
        {
            this.database = database;
            this.logger = logger;
        }

        /// <summary>
        /// Adds article.
        /// </summary>
        /// <param name="article">article</param>
        public void AddArticle(Article article)
        {
            System.Diagnostics.Contracts.Contract.Requires(article != null);
            logger.Verbose("Adding article with header: {@header}", article.Header);
            database.AddArticle(article);
        }

        /// <summary>
        /// Adds comment.
        /// </summary>
        /// <param name="articleId">Article identity</param>
        /// <param name="commentText">Comment text</param>
        /// <param name="userName">User name</param>
        public void AddComment(int articleId, string commentText, string userName)
        {
            logger.Verbose("Adding comment \"{@comment}\" from {@userName} to article {@articleId}", commentText, userName, articleId);
            database.AddComment(articleId, commentText, userName);
        }

        /// <summary>
        /// Deletes article.
        /// </summary>
        /// <param name="articleId">Article identity</param>
        public void DeleteArticle(int articleId)
        {
            logger.Verbose("Deleting article {@articleId}", articleId);
            database.DeleteArticle(articleId);
        }

        /// <summary>
        /// Gets article.
        /// </summary>
        /// <param name="articleId">Article identity</param>
        /// <returns>Article object</returns>
        public Article GetArticle(int articleId)
        {
            return database.GetArticle(articleId);
        }

        /// <summary>
        /// Gets articles.
        /// </summary>
        /// <returns>List of articles</returns>
        public ArticlePreview[] GetArticlePreviews()
        {
            return database.GetArticlePreviews().ToArray();
        }
    }
}
