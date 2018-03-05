using System;
using System.Runtime.Serialization;

namespace MiniBlog.DataContract
{
    /// <summary>
    /// Serializable fault object.
    /// </summary>
    [DataContract]
    public class ServiceFaultMessage
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ServiceFaultMessage()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Message</param>
        public ServiceFaultMessage(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Message.
        /// </summary>
        [DataMember]
        public string Message { get; set; }
    }
}