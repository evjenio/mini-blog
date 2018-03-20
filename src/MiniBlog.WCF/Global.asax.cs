using System;
using System.Web;
using Autofac;
using Autofac.Integration.Wcf;
using MiniBlog.Core;
using MiniBlog.Core.DataAccess;
using MiniBlog.Core.DataAccess.Infrastructure.NHibernate;
using MiniBlog.Core.DataAccess.Repositories;
using MiniBlog.Core.DataAccess.Repositories.NHibernate;
using MiniBlog.Core.DataAccess.UoW;
using MiniBlog.Core.DataAccess.UoW.NHibernate;
using MiniBlog.Core.Mappers;
using MiniBlog.WCF.ErrorHandling;
using MiniBlog.WCF.Infrastructure;
using MiniBlog.WCF.Services;
using NHibernate;
using Serilog;

namespace MiniBlog.WCF
{
    /// <inheritdoc />
    /// <summary>
    /// Component registration.
    /// </summary>
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();

            // Register service implementations.
            builder.RegisterType<BlogService>();

            // Register dependencies.

            // Register ISessionFactory as Singleton
            builder.Register(x => SessionFactoryConfigurator.Build()).SingleInstance();
            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession()).InstancePerLifetimeScope();

            builder.RegisterType<RepositoryResolver>().As<IRepositoryResolver>().SingleInstance();

            builder.RegisterType<PgConnectionFactory>().As<IConnectionFactory>().SingleInstance();
            builder.RegisterType<GlobalErrorHandler>().SingleInstance();
            builder.RegisterType<ObjectMapper>().As<IObjectMapper>().SingleInstance();

            builder.RegisterType<UnitOfWorkFactory>().As<IUnitOfWorkFactory>().SingleInstance();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));

            builder.RegisterType<Blog>().As<IBlog>().SingleInstance();

            builder.Register<ILogger>(c => new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .WriteTo.File("log.txt")
                    .WriteTo.Seq("http://localhost:5341")
                    .CreateLogger())
                .SingleInstance();

            // Set the dependency resolver.
            var container = builder.Build();
            AutofacHostFactory.Container = container;
        }
    }
}