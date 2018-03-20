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
            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Header);
            Property(x => x.Content, m => m.Lazy(true));
            OneToOne(x => x.Image,
                m =>
                {
                    m.Lazy(LazyRelation.Proxy);
                });
        }
    }
}
