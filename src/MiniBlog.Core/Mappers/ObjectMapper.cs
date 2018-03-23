using System;
using AutoMapper;
using MiniBlog.Contract;
using MiniBlog.Core.Domain;

namespace MiniBlog.Core.Mappers
{
    /// <summary>
    /// Dto mapper.
    /// </summary>
    public class ObjectMapper : IObjectMapper
    {
        static ObjectMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Article, ArticlePreviewDto>();
                cfg.CreateMap<ArticlePreviewDto, Article>();

                cfg.CreateMap<Article, ArticleDto>();
                cfg.CreateMap<ArticleDto, Article>();

                cfg.CreateMap<Comment, CommentDto>();
                cfg.CreateMap<CommentDto, Comment>();
            });
        }
        /// <summary>
        /// Map from source to TDestination.
        /// </summary>
        /// <typeparam name="TDestination">Destination type</typeparam>
        /// <param name="source">Source object</param>
        /// <returns>Destination object</returns>
        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }
    }
}
