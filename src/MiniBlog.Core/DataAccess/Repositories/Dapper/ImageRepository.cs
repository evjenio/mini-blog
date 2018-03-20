using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MiniBlog.Core.Domain;

namespace MiniBlog.Core.DataAccess.Repositories.Dapper
{
    /// <summary>
    /// Image Repository
    /// </summary>
    public class ImageRepository : Repository, IRepository<Image>
    {
        /// <summary>
        /// C-tor
        /// </summary>
        /// <param name="transaction">transaction</param>
        public ImageRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        /// <inheritdoc/>
        public void Add(Image entity)
        {
            var sql = "INSERT INTO public.images (imagefile) VALUES (@imagefile) RETURNING id";
            var id = Connection.QueryFirst<int>(sql, new { imagefile = entity.ImageFile }, Transaction);
            entity.Id = id;
        }

        /// <inheritdoc/>
        public void Delete(Image entity)
        {
            Connection.Execute("DELETE FROM public.images WHERE id = @id", new { id = entity.Id }, Transaction);
        }

        /// <inheritdoc/>
        public Image Get(int id)
        {
            return Connection.QuerySingleOrDefault<Image>("SELECT * FROM public.images WHERE id = @id", new { id }, Transaction);
        }

        /// <inheritdoc/>
        public IQueryable<Image> GetEntities()
        {
            return Connection.Query<Image>("SELECT * FROM public.images ORDER BY id ASC", Transaction).AsQueryable();
        }

        /// <inheritdoc/>
        public void Update(Image entity)
        {
            throw new NotImplementedException();
        }
    }
}
