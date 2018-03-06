﻿using System;
using System.Runtime.Serialization;

namespace MiniBlog.DataContract
{
    /// <summary>
    /// Comment.
    /// </summary>
    [DataContract]
    public class CommentDto
    {
        /// <summary>
        /// Comment text.
        /// </summary>
        [DataMember]
        public string CommentText { get; set; }

        /// <summary>
        /// UserName.
        /// </summary>
        [DataMember]
        public string UserName { get; set; }
    }
}