﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MiniBlog.ServiceClient.Integration {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Integration.IBlogService")]
    public interface IBlogService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBlogService/AddArticle", ReplyAction="http://tempuri.org/IBlogService/AddArticleResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(MiniBlog.DataContract.ServiceFaultMessage), Action="http://tempuri.org/IBlogService/AddArticleServiceFaultMessageFault", Name="ServiceFaultMessage", Namespace="http://schemas.datacontract.org/2004/07/MiniBlog.DataContract")]
        void AddArticle(MiniBlog.DataContract.ArticleDto article);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBlogService/AddArticle", ReplyAction="http://tempuri.org/IBlogService/AddArticleResponse")]
        System.Threading.Tasks.Task AddArticleAsync(MiniBlog.DataContract.ArticleDto article);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBlogService/AddComment", ReplyAction="http://tempuri.org/IBlogService/AddCommentResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(MiniBlog.DataContract.ServiceFaultMessage), Action="http://tempuri.org/IBlogService/AddCommentServiceFaultMessageFault", Name="ServiceFaultMessage", Namespace="http://schemas.datacontract.org/2004/07/MiniBlog.DataContract")]
        void AddComment(int articleId, string commentText, string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBlogService/AddComment", ReplyAction="http://tempuri.org/IBlogService/AddCommentResponse")]
        System.Threading.Tasks.Task AddCommentAsync(int articleId, string commentText, string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBlogService/DeleteArticle", ReplyAction="http://tempuri.org/IBlogService/DeleteArticleResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(MiniBlog.DataContract.ServiceFaultMessage), Action="http://tempuri.org/IBlogService/DeleteArticleServiceFaultMessageFault", Name="ServiceFaultMessage", Namespace="http://schemas.datacontract.org/2004/07/MiniBlog.DataContract")]
        void DeleteArticle(int articleId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBlogService/DeleteArticle", ReplyAction="http://tempuri.org/IBlogService/DeleteArticleResponse")]
        System.Threading.Tasks.Task DeleteArticleAsync(int articleId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBlogService/GetArticle", ReplyAction="http://tempuri.org/IBlogService/GetArticleResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(MiniBlog.DataContract.ServiceFaultMessage), Action="http://tempuri.org/IBlogService/GetArticleServiceFaultMessageFault", Name="ServiceFaultMessage", Namespace="http://schemas.datacontract.org/2004/07/MiniBlog.DataContract")]
        MiniBlog.DataContract.ArticleDto GetArticle(int articleId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBlogService/GetArticle", ReplyAction="http://tempuri.org/IBlogService/GetArticleResponse")]
        System.Threading.Tasks.Task<MiniBlog.DataContract.ArticleDto> GetArticleAsync(int articleId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBlogService/GetArticlePreviews", ReplyAction="http://tempuri.org/IBlogService/GetArticlePreviewsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(MiniBlog.DataContract.ServiceFaultMessage), Action="http://tempuri.org/IBlogService/GetArticlePreviewsServiceFaultMessageFault", Name="ServiceFaultMessage", Namespace="http://schemas.datacontract.org/2004/07/MiniBlog.DataContract")]
        MiniBlog.DataContract.ArticlePreviewDto[] GetArticlePreviews();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBlogService/GetArticlePreviews", ReplyAction="http://tempuri.org/IBlogService/GetArticlePreviewsResponse")]
        System.Threading.Tasks.Task<MiniBlog.DataContract.ArticlePreviewDto[]> GetArticlePreviewsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBlogServiceChannel : MiniBlog.ServiceClient.Integration.IBlogService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BlogServiceClient : System.ServiceModel.ClientBase<MiniBlog.ServiceClient.Integration.IBlogService>, MiniBlog.ServiceClient.Integration.IBlogService {
        
        public BlogServiceClient() {
        }
        
        public BlogServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BlogServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BlogServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BlogServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void AddArticle(MiniBlog.DataContract.ArticleDto article) {
            base.Channel.AddArticle(article);
        }
        
        public System.Threading.Tasks.Task AddArticleAsync(MiniBlog.DataContract.ArticleDto article) {
            return base.Channel.AddArticleAsync(article);
        }
        
        public void AddComment(int articleId, string commentText, string userName) {
            base.Channel.AddComment(articleId, commentText, userName);
        }
        
        public System.Threading.Tasks.Task AddCommentAsync(int articleId, string commentText, string userName) {
            return base.Channel.AddCommentAsync(articleId, commentText, userName);
        }
        
        public void DeleteArticle(int articleId) {
            base.Channel.DeleteArticle(articleId);
        }
        
        public System.Threading.Tasks.Task DeleteArticleAsync(int articleId) {
            return base.Channel.DeleteArticleAsync(articleId);
        }
        
        public MiniBlog.DataContract.ArticleDto GetArticle(int articleId) {
            return base.Channel.GetArticle(articleId);
        }
        
        public System.Threading.Tasks.Task<MiniBlog.DataContract.ArticleDto> GetArticleAsync(int articleId) {
            return base.Channel.GetArticleAsync(articleId);
        }
        
        public MiniBlog.DataContract.ArticlePreviewDto[] GetArticlePreviews() {
            return base.Channel.GetArticlePreviews();
        }
        
        public System.Threading.Tasks.Task<MiniBlog.DataContract.ArticlePreviewDto[]> GetArticlePreviewsAsync() {
            return base.Channel.GetArticlePreviewsAsync();
        }
    }
}
