using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MiniBlog.DataContract
{
    /// <summary>
    /// Article.
    /// </summary>
    [DataContract]
    public class ArticleDto : ArticlePreviewDto
    {
        /// <summary>
        /// List of comments.
        /// </summary>
        [DataMember]
        public List<CommentDto> Comments { get; set; }

        /// <summary>
        /// Article content.
        /// </summary>
        [DataMember]
        public string Content { get; set; }

        /// <summary>
        /// Image.
        /// </summary>
        [DataMember]
        public byte[] Image { get; set; }
    }
}
