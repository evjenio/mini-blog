using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using MiniBlog.Core.Domain;

namespace MiniBlog.Core.DataAccess.Repositories.Dapper
{
    public class ImageRepository : Repository, IImageRepository
    {
        public ImageRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public int Add(Image entity)
        {
            var sql = "INSERT INTO public.images (imagefile) VALUES (@imagefile) RETURNING id";
            var id = Connection.QueryFirst<int>(sql, new { imagefile = entity.ImageFile }, Transaction);
            entity.Id = id;
            return id;
        }

        public void Delete(Image entity)
        {
            Connection.Execute("DELETE FROM public.images WHERE id = @id", new { id = entity.Id }, Transaction);
        }

        public void Delete(int id)
        {
            Connection.Execute("DELETE FROM public.images WHERE id = @id", new { id }, Transaction);
        }

        public Image Get(int id)
        {
            return Connection.QuerySingleOrDefault<Image>("SELECT * FROM public.images WHERE id = @id", new { id }, Transaction);
        }

        public IEnumerable<Image> GetEntities()
        {
            return Connection.Query<Image>("SELECT * FROM public.images ORDER BY id ASC", Transaction);
        }

        public void Update(Image entity)
        {
            throw new NotImplementedException();
        }
    }
}
