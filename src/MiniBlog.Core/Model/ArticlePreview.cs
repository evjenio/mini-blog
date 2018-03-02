using System.Runtime.Serialization;

namespace MiniBlog.Core.Model
{
    [DataContract]
    public class ArticlePreview
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Header { get; set; }
    }
}
