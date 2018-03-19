using System;

namespace MiniBlog.Core.DataAccess.UoW.NHibernate
{
    /// <inheritdoc />
    /// <summary>
    /// Unit Of Work Factory (NHibernate)
    /// </summary>
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        /// <inheritdoc />
        public IUnitOfWork Create() => new UnitOfWork();
    }
}
