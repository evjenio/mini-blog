using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MiniBlog.Core.Model
{
    [DataContract]
    public class Article : ArticlePreview
    {
        [DataMember]
        public byte[] Image { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public List<Comment> Comments { get; set; }
    }
}
