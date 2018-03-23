using MiniBlog.Core.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MiniBlog.Core.DataAccess.Repositories.NHibernate.Mappings
{
    public class ArticleMap : ClassMapping<Article>
    {
        public ArticleMap()
        {
            Table("articles");
            Id(x => x.Id,
                m => m.Generator(Generators.Identity,
                    g => g.Params(new
                    {
                        sequence = "articles_id_seq"
                    })));
            Property(x => x.Header);
            Property(x => x.Content, m => m.Lazy(true));
            Set(x => x.Comments,
                map =>
                {
                    map.Key(k => k.Column("ArticleId"));
                    map.Fetch(CollectionFetchMode.Subselect);
                    map.Lazy(CollectionLazy.Lazy);
                },
                m => m.OneToMany(p => p.Class(typeof(Comment))));
            ManyToOne(x => x.Image,
                m =>
                {
                    m.Column("ImageId");
                });
        }
    }
}
