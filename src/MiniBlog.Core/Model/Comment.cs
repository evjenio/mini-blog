using System.Runtime.Serialization;

namespace MiniBlog.Core.Model
{
    [DataContract]
    public class Comment
    {
        [DataMember]
        public string User { get; set; }

        [DataMember]
        public string Text { get; set; }
    }
}
