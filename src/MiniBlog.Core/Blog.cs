using System;
using System.Collections.Generic;
using MiniBlog.Contract;
using MiniBlog.Core.DataAccess;
using MiniBlog.Core.Domain;
using MiniBlog.Core.Mappers;
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
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            logger.Verbose("Adding article with header: {@header}", article.Header);
            var mappedArticle = objectMapper.Map<ArticleDto, Article>(article);

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                article.Image.ToOption()
                    .Do(file =>
                    {
                        var img = new Image() { ImageFile = file };
                        unitOfWork.ImageRepository.Add(img);
                        mappedArticle.ImageId = img.Id;
                    });

                unitOfWork.ArticleRepository.Add(mappedArticle);
                unitOfWork.Commit();
            }
        }

        public void AddComment(CommentDto comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

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
                var mappedArticle = objectMapper.Map<Article, ArticleDto>(article);

                if (article.ImageId.HasValue)
                {
                    var image = unitOfWork.ImageRepository.Get(article.ImageId.Value);
                    mappedArticle.Image = image.ImageFile;
                }

                IEnumerable<Comment> comments = unitOfWork.CommentRepository.GetCommentsForArticle(articleId);
                List<CommentDto> mappedComments = objectMapper.Map<IEnumerable<Comment>, List<CommentDto>>(comments);
                mappedArticle.Comments = mappedComments;

                unitOfWork.Commit();
                return mappedArticle;
            }
        }

        public ArticlePreviewDto[] GetArticlePreviews()
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                IEnumerable<Article> articles = unitOfWork.ArticleRepository.GetEntities();
                unitOfWork.Commit();
                return objectMapper.Map<IEnumerable<Article>, ArticlePreviewDto[]>(articles);
            }
        }
    }
}
