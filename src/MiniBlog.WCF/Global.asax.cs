using System;
using System.Web;
using Autofac;
using Autofac.Integration.Wcf;
using MiniBlog.Core.DataAccess;
using MiniBlog.WCF.ErrorHandling;
using MiniBlog.WCF.Services;
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
            builder.RegisterType<Database>();
            builder.RegisterType<PgConnectionFactory>().As<IConnectionFactory>();
            builder.RegisterType<GlobalErrorHandler>();

            builder.Register<ILogger>(c => new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .WriteTo.File("log.txt")
                    .CreateLogger())
                .SingleInstance();

            // Set the dependency resolver.
            var container = builder.Build();
            AutofacHostFactory.Container = container;
        }
    }
}