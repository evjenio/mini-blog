using System;
using System.Collections.Generic;
using System.Linq;
using MiniBlog.Contract;
using MiniBlog.Core.DataAccess.UoW;
using MiniBlog.Core.Domain;
using MiniBlog.Core.Mappers;
using Nelibur.Sword.Extensions;
using NHibernate.Linq;
using Serilog;

namespace MiniBlog.Core
{
    public class Blog : IBlog
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IObjectMapper objectMapper;
        private readonly ILogger logger;

        public Blog(IUnitOfWorkFactory unitOfWorkFactory, IObjectMapper objectMapper, ILogger logger)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.objectMapper = objectMapper;
            this.logger = logger;
        }

        public void AddArticle(ArticleDto article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            logger.Verbose("Adding article: {@article}", article);
            var mappedArticle = objectMapper.Map<ArticleDto, Article>(article);

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                article.Image.ToOption()
                    .Do(file => mappedArticle.Image = new Image() { ImageFile = file });

                unitOfWork.RepositoryFor<Article>().Add(mappedArticle);
                unitOfWork.Commit();
            }
        }

        public void AddComment(CommentDto comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            logger.Verbose("Adding comment {@comment}", comment);

            var mappedComment = objectMapper.Map<CommentDto, Comment>(comment);

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var article = unitOfWork.RepositoryFor<Article>().Get(comment.ArticleId);
                article.Comments.Add(mappedComment);
                unitOfWork.RepositoryFor<Article>().Update(article);
                unitOfWork.Commit();
            }
        }

        public void DeleteArticle(int articleId)
        {
            logger.Verbose("Deleting article {articleId}", articleId);
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var article = unitOfWork.RepositoryFor<Article>().Get(articleId);
                unitOfWork.RepositoryFor<Article>().Delete(article);
                unitOfWork.Commit();
            }
        }

        public ArticleDto GetArticle(int articleId)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var article = unitOfWork.RepositoryFor<Article>()
                    .GetEntities()
                    .Fetch(x => x.Comments)
                    .FirstOrDefault(a => a.Id == articleId);

                unitOfWork.Commit();
                var mappedArticle = objectMapper.Map<Article, ArticleDto>(article);
                return mappedArticle;
            }
        }

        public ArticlePreviewDto[] GetArticlePreviews()
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                IEnumerable<Article> articles = unitOfWork.RepositoryFor<Article>()
                    .GetEntities().ToList();
                unitOfWork.Commit();
                return objectMapper.Map<IEnumerable<Article>, ArticlePreviewDto[]>(articles);
            }
        }
    }
}
