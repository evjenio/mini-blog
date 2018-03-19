using System;
using MiniBlog.Core.Domain;

namespace MiniBlog.Core.DataAccess.Repositories
{
    /// <summary>
    /// Image Repository
    /// </summary>
    public interface IImageRepository : IRepository<Image, int>
    {
    }
}
