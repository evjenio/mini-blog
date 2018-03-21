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
    /// <summary>
    /// Blog (service).
    /// </summary>
    public class Blog : IBlog
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IObjectMapper objectMapper;
        private readonly ILogger logger;

        /// <summary>
        /// C-tor.
        /// </summary>
        /// <param name="unitOfWorkFactory">UoW factory.</param>
        /// <param name="objectMapper">Object mapper.</param>
        /// <param name="logger">Logger.</param>
        public Blog(IUnitOfWorkFactory unitOfWorkFactory, IObjectMapper objectMapper, ILogger logger)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.objectMapper = objectMapper;
            this.logger = logger;
        }

        /// <summary>
        /// Adds article.
        /// </summary>
        public void AddArticle(ArticleDto article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            logger.Verbose("Adding article: {@article}", article);
            var mappedArticle = objectMapper.Map<Article>(article);

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                article.Image.ToOption()
                    .Do(file => mappedArticle.Image = new Image() { ImageFile = file });

                unitOfWork.RepositoryFor<Article>().Add(mappedArticle);
                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Adds comment.
        /// </summary>
        /// <param name="comment">Comment object</param>
        public void AddComment(CommentDto comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            logger.Verbose("Adding comment {@comment}", comment);

            var mappedComment = objectMapper.Map<Comment>(comment);

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var article = unitOfWork.RepositoryFor<Article>().Get(comment.ArticleId);
                article.Comments.Add(mappedComment);
                unitOfWork.RepositoryFor<Article>().Update(article);
                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Deletes article.
        /// </summary>
        /// <param name="articleId">Article identity</param>
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

        /// <summary>
        /// Gets article.
        /// </summary>
        /// <param name="articleId">Article identity</param>
        /// <returns>Article object</returns>
        public ArticleDto GetArticle(int articleId)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var article = unitOfWork.RepositoryFor<Article>().Get(articleId);
                unitOfWork.Commit();
                var mappedArticle = objectMapper.Map<ArticleDto>(article);
                return mappedArticle;
            }
        }

        /// <summary>
        /// Gets articles.
        /// </summary>
        /// <returns>List of articles</returns>
        public ArticlePreviewDto[] GetArticlePreviews()
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                IEnumerable<Article> articles = unitOfWork.RepositoryFor<Article>()
                    .All()
                    .Fetch(x => x.Image)
                    .ToList();
                unitOfWork.Commit();
                return objectMapper.Map<ArticlePreviewDto[]>(articles);
            }
        }
    }
}
