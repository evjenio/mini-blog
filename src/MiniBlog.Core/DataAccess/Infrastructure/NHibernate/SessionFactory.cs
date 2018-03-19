using System;
using System.Threading;
using NHibernate;

namespace MiniBlog.Core.DataAccess.Infrastructure.NHibernate
{
    /// <summary>
    /// NHibernate Session Factory
    /// </summary>
    public class SessionFactory
    {
        private static readonly Lazy<ISessionFactory> instance = new Lazy<ISessionFactory>(() => Build(), LazyThreadSafetyMode.ExecutionAndPublication);

        private static ISessionFactory Build()
        {
            var configuration = new global::NHibernate.Cfg.Configuration();
            configuration.Configure();
            return configuration.BuildSessionFactory();
        }
        /// <summary>
        /// Default instance
        /// </summary>
        public ISessionFactory Instance => instance.Value;
    }
}
