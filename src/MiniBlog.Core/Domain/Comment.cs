using System;

namespace MiniBlog.Core.Domain
{
    /// <summary>
    /// Comment.
    /// </summary>
    public class Comment : IEntity<int>
    {
        /// <summary>
        /// Identity.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Comment text.
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// UserName.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Article Identity.
        /// </summary>
        public int ArticleId { get; set; }
    }
}
