using System;
using MiniBlog.Core.DataAccess.Repositories.NHibernate.Mappings;
using NHibernate;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;

namespace MiniBlog.Core.DataAccess.Infrastructure.NHibernate
{
    /// <summary>
    /// NHibernate Session Factory
    /// </summary>
    public static class SessionFactoryConfigurator
    {
        public static ISessionFactory Build()
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(typeof(ArticleMap).Assembly.ExportedTypes);
            HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var configuration = new global::NHibernate.Cfg.Configuration();
            configuration.AddMapping(mapping);
            configuration.Configure();
            return configuration.BuildSessionFactory();
        }
    }
}
