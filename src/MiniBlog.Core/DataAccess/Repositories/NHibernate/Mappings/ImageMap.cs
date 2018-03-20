using MiniBlog.Core.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MiniBlog.Core.DataAccess.Repositories.NHibernate.Mappings
{
    public class ImageMap : ClassMapping<Image>
    {
        public ImageMap()
        {
            Table("images");
            Id(x => x.Id, m => m.Generator(Generators.Foreign<Image>(i => i.Article)));
            Property(x => x.ImageFile);
        }
    }
}
