using System.ServiceModel;
using MiniBlog.Core.Model;

namespace MiniBlog.WCF.Services
{
    [ServiceContract]
    public interface IBlogService
    {
        [OperationContract]
        void AddArticle(Article article);

        [OperationContract]
        void AddComment(int articleId);

        [OperationContract]
        void DeleteArticle(int articleId);

        [OperationContract]
        Article GetArticle(int articleId);

        [OperationContract]
        ArticlePreview[] GetArticlePreviews();
    }
}