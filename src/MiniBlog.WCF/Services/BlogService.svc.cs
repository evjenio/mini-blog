using System;
using System.Diagnostics.Contracts;
using System.Linq;
using MiniBlog.Core.DataAccess;
using MiniBlog.Core.DataAccess.Model;
using MiniBlog.Core.Mappers;
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
        private readonly IObjectMapper objectMapper;
        private readonly ILogger logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="database">DB</param>
        /// <param name="objectMapper"></param>
        /// <param name="logger">Logger</param>
        public BlogService(Database database, IObjectMapper objectMapper, ILogger logger)
        {
            this.database = database;
            this.objectMapper = objectMapper;
            this.logger = logger;
        }

        /// <summary>
        /// Adds article.
        /// </summary>
        /// <param name="article">article</param>
        public void AddArticle(ArticleDto article)
        {
            Contract.Requires(article != null);
            logger.Verbose("Adding article with header: {@header}", article.Header);
            var mappedArticle = objectMapper.Map<ArticleDto, Article>(article);
            database.AddArticle(mappedArticle);
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
        public ArticleDto GetArticle(int articleId)
        {
            var article = database.GetArticle(articleId);
            return objectMapper.Map<Article, ArticleDto>(article);
        }

        /// <summary>
        /// Gets articles.
        /// </summary>
        /// <returns>List of articles</returns>
        public ArticlePreviewDto[] GetArticlePreviews()
        {
            ArticlePreview[] articles = database.GetArticlePreviews().ToArray();
            return objectMapper.Map<ArticlePreview[], ArticlePreviewDto[]>(articles);
        }
    }
}
