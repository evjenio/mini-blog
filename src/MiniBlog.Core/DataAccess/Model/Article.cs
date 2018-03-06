using System;
using System.Collections.Generic;

namespace MiniBlog.Core.DataAccess.Model
{
    /// <summary>
    /// Article.
    /// </summary>
    public class Article : ArticlePreview
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
        /// Image.
        /// </summary>
        public byte[] Image { get; set; }
    }
}
