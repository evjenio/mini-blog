using System;
using NHibernate;

namespace MiniBlog.Core.DataAccess.Repositories.NHibernate
{
    /// <summary>
    /// Abstract repository (NHibernate)
    /// </summary>
    public abstract class Repository
    {
        /// <summary>
        /// Session
        /// </summary>
        protected ISession Session { get; }

        /// <summary>
        /// C-tor
        /// </summary>
        /// <param name="session">session</param>
        protected Repository(ISession session)
        {
            Session = session;
        }
    }
}
