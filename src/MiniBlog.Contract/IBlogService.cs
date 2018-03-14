using System;
using System.ServiceModel;

namespace MiniBlog.Contract
{
    /// <summary>
    /// Blog WCF service contract.
    /// </summary>
    [ServiceContract]
    public interface IBlogService
    {
        /// <summary>
        /// Adds article.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(ServiceFaultMessage))]
        void AddArticle(ArticleDto article);

        /// <summary>
        /// Adds comment.
        /// </summary>
        /// <param name="comment">Comment object</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFaultMessage))]
        void AddComment(CommentDto comment);

        /// <summary>
        /// Deletes article.
        /// </summary>
        /// <param name="articleId">Article identity</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFaultMessage))]
        void DeleteArticle(int articleId);

        /// <summary>
        /// Gets article.
        /// </summary>
        /// <param name="articleId">Article identity</param>
        /// <returns>Article object</returns>
        [OperationContract]
        [FaultContract(typeof(ServiceFaultMessage))]
        ArticleDto GetArticle(int articleId);

        /// <summary>
        /// Gets articles.
        /// </summary>
        /// <returns>List of articles</returns>
        [OperationContract]
        [FaultContract(typeof(ServiceFaultMessage))]
        ArticlePreviewDto[] GetArticlePreviews();
    }
}
