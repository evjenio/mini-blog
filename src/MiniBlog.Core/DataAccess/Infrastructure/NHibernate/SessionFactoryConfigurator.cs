using System;
using System.Data;
using MiniBlog.Core.DataAccess.Repositories.NHibernate.Mappings;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;

namespace MiniBlog.Core.DataAccess.Infrastructure.NHibernate
{
    /// <summary>
    /// NHibernate Session Factory
    /// </summary>
    public static class SessionFactoryConfigurator
    {
        public static ISessionFactory Build()
        {
            var configuration = new Configuration();
            configuration.DataBaseIntegration(db =>
            {
                db.Dialect<PostgreSQLDialect>();
                db.Driver<NpgsqlDriver>();
                db.ConnectionProvider<DriverConnectionProvider>();
                db.IsolationLevel = IsolationLevel.ReadCommitted;

                db.ConnectionStringName = "database";
                db.Timeout = 10;

                // enabled for testing
                db.LogFormattedSql = true;
                db.LogSqlInConsole = true;
                db.AutoCommentSql = true;
            });

            var mapper = new ModelMapper();
            mapper.AddMappings(typeof(ArticleMap).Assembly.ExportedTypes);
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            configuration.AddMapping(mapping);
            SchemaMetadataUpdater.QuoteTableAndColumns(configuration, new PostgreSQLDialect());
            return configuration.BuildSessionFactory();
        }
    }
}
