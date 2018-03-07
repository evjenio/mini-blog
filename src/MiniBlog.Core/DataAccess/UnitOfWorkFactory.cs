using System;
using MiniBlog.Core.DataAccess.Repositories;

namespace MiniBlog.Core.DataAccess
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IConnectionFactory connectionFactory;
        private readonly IRepositoryFactory repositoryFactory;

        public UnitOfWorkFactory(IConnectionFactory connectionFactory, IRepositoryFactory repositoryFactory)
        {
            this.connectionFactory = connectionFactory;
            this.repositoryFactory = repositoryFactory;
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(connectionFactory, repositoryFactory);
        }
    }
}
