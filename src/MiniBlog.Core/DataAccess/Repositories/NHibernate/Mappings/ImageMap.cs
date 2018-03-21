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
            Id(x => x.Id,
                m => m.Generator(Generators.Identity,
                    g => g.Params(new
                    {
                        sequence = "images_id_seq"
                    })));
            Property(x => x.ImageFile);
        }
    }
}
