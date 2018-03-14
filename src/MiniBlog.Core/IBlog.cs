using MiniBlog.Contract;

namespace MiniBlog.Core
{
    public interface IBlog
    {
        /// <summary>
        /// Adds article.
        /// </summary>
        void AddArticle(ArticleDto article);

        /// <summary>
        /// Adds comment.
        /// </summary>
        /// <param name="comment">Comment object</param>
        void AddComment(CommentDto comment);

        /// <summary>
        /// Deletes article.
        /// </summary>
        /// <param name="articleId">Article identity</param>
        void DeleteArticle(int articleId);

        /// <summary>
        /// Gets article.
        /// </summary>
        /// <param name="articleId">Article identity</param>
        /// <returns>Article object</returns>
        ArticleDto GetArticle(int articleId);

        /// <summary>
        /// Gets articles.
        /// </summary>
        /// <returns>List of articles</returns>
        ArticlePreviewDto[] GetArticlePreviews();
    }
}
