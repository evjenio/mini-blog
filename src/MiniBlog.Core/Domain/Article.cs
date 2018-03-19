using System;
using System.Collections.Generic;

namespace MiniBlog.Core.Domain
{
    /// <summary>
    /// Article.
    /// </summary>
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class Article : IEntity<int>
    {
        /// <summary>
        /// Article content.
        /// </summary>
        public virtual string Content { get; set; }

        /// <summary>
        /// Article header.
        /// </summary>
        public virtual string Header { get; set; }

        /// <summary>
        /// Article identity.
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Image.
        /// </summary>
        public virtual int? ImageId { get; set; }
    }
}
