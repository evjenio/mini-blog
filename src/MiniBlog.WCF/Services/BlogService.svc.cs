using System;
using System.Diagnostics.Contracts;
using MiniBlog.Contract;
using MiniBlog.Core;
using MiniBlog.WCF.ErrorHandling;

namespace MiniBlog.WCF.Services
{
    /// <summary>
    /// Blog Service.
    /// </summary>
    [GlobalErrorBehavior(typeof(GlobalErrorHandler))]
    public class BlogService : IBlogService
    {
        private readonly IBlog blog;

        /// <summary>
        /// Constructor.
        /// </summary>
        public BlogService(IBlog blog)
        {
            this.blog = blog;
        }

        /// <summary>
        /// Adds article.
        /// </summary>
        /// <param name="article">article</param>
        public void AddArticle(ArticleDto article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            blog.AddArticle(article);
        }

        /// <summary>
        /// Adds comment.
        /// </summary>
        /// <param name="comment">Comment object</param>
        public void AddComment(CommentDto comment)
        {
            blog.AddComment(comment);
        }

        /// <summary>
        /// Deletes article.
        /// </summary>
        /// <param name="articleId">Article identity</param>
        public void DeleteArticle(int articleId)
        {
            blog.DeleteArticle(articleId);
        }

        /// <summary>
        /// Gets article.
        /// </summary>
        /// <param name="articleId">Article identity</param>
        /// <returns>Article object</returns>
        public ArticleDto GetArticle(int articleId)
        {
            return blog.GetArticle(articleId);
        }

        /// <summary>
        /// Gets articles.
        /// </summary>
        /// <returns>List of articles</returns>
        public ArticlePreviewDto[] GetArticlePreviews()
        {
            return blog.GetArticlePreviews();
        }
    }
}
