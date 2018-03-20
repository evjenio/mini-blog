using System;
using MiniBlog.Core.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MiniBlog.Core.DataAccess.Repositories.NHibernate.Mappings
{
    public class CommentMap : ClassMapping<Comment>
    {
        public CommentMap()
        {
            Table("comments");
            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.CommentText);
            Property(x => x.UserName);
            ManyToOne(comment => comment.Article, m =>
            {
                m.Cascade(Cascade.All);
                m.Fetch(FetchKind.Join);
                m.Column("ArticleId");
            });
        }
    }
}
