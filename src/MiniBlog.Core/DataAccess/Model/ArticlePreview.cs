using System;

namespace MiniBlog.Core.DataAccess.Model
{
    /// <summary>
    /// Article preview.
    /// </summary>
    public class ArticlePreview
    {
        /// <summary>
        /// Article header.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Article identity.
        /// </summary>
        public int Id { get; set; }
    }
}
