using System;

namespace MiniBlog.Core.DataAccess
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
