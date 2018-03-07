using System;
using MiniBlog.Core.Domain;

namespace MiniBlog.Core.DataAccess.Repositories
{
    public interface IImageRepository : IRepository<Image, int>
    {
    }
}
