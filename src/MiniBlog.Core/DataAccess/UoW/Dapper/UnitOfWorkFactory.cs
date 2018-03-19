using System;

namespace MiniBlog.Core.DataAccess.UoW.Dapper
{
    /// <inheritdoc />
    /// <summary>
    /// Unit of work factory (Dapper)
    /// </summary>
    [Obsolete("Use MiniBlog.Core.DataAccess.UoW.NHibernate.UnitOfWorkFactory")]
    // ReSharper disable once UnusedMember.Global
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IConnectionFactory connectionFactory;

        /// <summary>
        /// C-tor
        /// </summary>
        /// <param name="connectionFactory">Connection factory</param>
        public UnitOfWorkFactory(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        /// <inheritdoc />
        public IUnitOfWork Create()
        {
            return new UnitOfWork(connectionFactory);
        }
    }
}
