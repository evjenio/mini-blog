using System;
using System.Runtime.Serialization;

namespace MiniBlog.DataContract
{
    /// <summary>
    /// Article preview.
    /// </summary>
    [DataContract]
    public class ArticlePreview
    {
        /// <summary>
        /// Article header.
        /// </summary>
        [DataMember]
        public string Header { get; set; }

        /// <summary>
        /// Article identity.
        /// </summary>
        [DataMember]
        public int Id { get; set; }
    }
}
