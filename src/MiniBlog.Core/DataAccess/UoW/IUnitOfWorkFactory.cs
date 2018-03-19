using System;

namespace MiniBlog.Core.DataAccess.UoW
{
    /// <summary>
    /// Factory of UoW
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// Create UoW
        /// </summary>
        /// <returns>UoW instance</returns>
        IUnitOfWork Create();
    }
}
