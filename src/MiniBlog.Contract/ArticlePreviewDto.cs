﻿using System;
using System.Runtime.Serialization;

namespace MiniBlog.Contract
{
    /// <summary>
    /// Article preview.
    /// </summary>
    [DataContract]
    public class ArticlePreviewDto
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
