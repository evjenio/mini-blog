﻿using System;
using System.Collections.Generic;
using System.Linq;
using MiniBlog.Core.Domain;
using NHibernate;

namespace MiniBlog.Core.DataAccess.Repositories.NHibernate
{
    /// <summary>
    /// Image repository
    /// </summary>
    public class ImageRepository : Repository, IImageRepository
    {
        /// <summary>
        /// C-tor
        /// </summary>
        /// <param name="session">Session</param>
        public ImageRepository(ISession session)
            : base(session)
        {
        }

        /// <inheritdoc/>
        public int Add(Image entity)
        {
            Session.Save(entity);
            return entity.Id;
        }

        /// <inheritdoc/>
        public void Delete(Image entity)
        {
            Session.Delete(entity);
        }

        /// <inheritdoc/>
        public void Delete(int id)
        {
            Session.Delete(new Image() { Id = id });
        }

        /// <inheritdoc/>
        public Image Get(int id)
        {
            return Session.Get<Image>(id);
        }

        /// <inheritdoc/>
        public IEnumerable<Image> GetEntities()
        {
            return Session.Query<Image>().ToList();
        }

        /// <inheritdoc/>
        public void Update(Image entity)
        {
            Session.Update(entity);
        }
    }
}
