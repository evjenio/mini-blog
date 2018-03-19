using System;

namespace MiniBlog.Core.Domain
{
    /// <summary>
    /// Image.
    /// </summary>
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class Image : IEntity<int>
    {
        /// <summary>
        /// Identity.
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Image.
        /// </summary>
        public virtual byte[] ImageFile { get; set; }
    }
}
