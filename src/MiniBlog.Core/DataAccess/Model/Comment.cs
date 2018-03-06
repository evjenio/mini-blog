using System;

namespace MiniBlog.Core.DataAccess.Model
{
    /// <summary>
    /// Comment.
    /// </summary>
    public class Comment
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
    }
}
