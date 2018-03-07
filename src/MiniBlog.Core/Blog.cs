using System;
using System.Diagnostics.Contracts;
using MiniBlog.Core.DataAccess;
using MiniBlog.Core.Domain;
using MiniBlog.Core.Mappers;
using MiniBlog.DataContract;
using Nelibur.Sword.Extensions;
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
            Contract.Requires(article != null);
            logger.Verbose("Adding article with header: {@header}", article.Header);
            var mappedArticle = objectMapper.Map<ArticleDto, Article>(article);

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                article.Image.ToOption()
                    .Do(file =>
                    {
                        var img = new Image() { ImageFile = file };
                        unitOfWork.ImageRepository.Add(img);
                        mappedArticle.Image = img;
                    });

                unitOfWork.ArticleRepository.Add(mappedArticle);
                unitOfWork.Commit();
            }
        }

        public void AddComment(CommentDto comment)
        {
            Contract.Requires(comment != null);
            logger.Verbose("Adding comment \"{@comment}\" from {@userName} to article {@articleId}",
                comment.CommentText, comment.UserName, comment.ArticleId);

            var mappedComment = objectMapper.Map<CommentDto, Comment>(comment);

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                unitOfWork.CommentRepository.Add(mappedComment);
                unitOfWork.Commit();
            }
        }

        public void DeleteArticle(int articleId)
        {
            logger.Verbose("Deleting article {@articleId}", articleId);
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                unitOfWork.ArticleRepository.Delete(articleId);
                unitOfWork.Commit();
            }
        }

        public ArticleDto GetArticle(int articleId)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var article = unitOfWork.ArticleRepository.Get(articleId);
                unitOfWork.Commit();
                return objectMapper.Map<Article, ArticleDto>(article);
            }
        }

        public ArticlePreviewDto[] GetArticlePreviews()
        {
            throw new NotImplementedException();
        }
    }
}
