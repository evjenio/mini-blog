using System;
using System.Collections.Generic;

namespace MiniBlog.Core.Domain
{
    /// <summary>
    /// Article.
    /// </summary>
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class Article : IEntity
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
        public virtual Image Image { get; set; }

        /// <summary>
        /// Comments.
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
