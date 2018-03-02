using System;
using MiniBlog.Core.Model;

namespace MiniBlog.WCF.Services
{
    public class BlogService : IBlogService
    {
        public void AddArticle(Article article)
        {
            throw new NotImplementedException();
        }

        public void AddComment(int articleId)
        {
            throw new NotImplementedException();
        }

        public void DeleteArticle(int articleId)
        {
            throw new NotImplementedException();
        }

        public Article GetArticle(int articleId)
        {
            throw new NotImplementedException();
        }

        public ArticlePreview[] GetArticlePreviews()
        {
            throw new NotImplementedException();
        }
    }
}