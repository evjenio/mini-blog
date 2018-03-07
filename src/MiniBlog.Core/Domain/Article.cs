using System;
using System.Collections.Generic;

namespace MiniBlog.Core.Domain
{
    /// <summary>
    /// Article.
    /// </summary>
    public class Article : IEntity<int>
    {
        /// <summary>
        /// List of comments.
        /// </summary>
        public List<Comment> Comments { get; set; }

        /// <summary>
        /// Article content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Article header.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Article identity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Image.
        /// </summary>
        public Image Image { get; set; }
    }
}
