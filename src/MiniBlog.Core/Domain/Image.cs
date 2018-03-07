using System;

namespace MiniBlog.Core.Domain
{
    /// <summary>
    /// Image.
    /// </summary>
    public class Image : IEntity<int>
    {
        /// <summary>
        /// Identity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Image.
        /// </summary>
        public byte[] ImageFile { get; set; }
    }
}
