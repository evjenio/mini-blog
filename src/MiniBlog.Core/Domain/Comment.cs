using System;

namespace MiniBlog.Core.Domain
{
    /// <summary>
    /// Comment.
    /// </summary>
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class Comment : IEntity<int>
    {
        /// <summary>
        /// Identity.
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// Comment text.
        /// </summary>
        public virtual string CommentText { get; set; }

        /// <summary>
        /// UserName.
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// Article Identity.
        /// </summary>
        public virtual int ArticleId { get; set; }
    }
}
