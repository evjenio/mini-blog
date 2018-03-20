using System;

namespace MiniBlog.Core.Domain
{
    /// <summary>
    /// Image.
    /// </summary>
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class Image : IEntity
    {
        /// <summary>
        /// Identity.
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Image.
        /// </summary>
        public virtual byte[] ImageFile { get; set; }

        /// <summary>
        /// Article.
        /// </summary>
        public virtual Article Article { get; set; }
    }
}
