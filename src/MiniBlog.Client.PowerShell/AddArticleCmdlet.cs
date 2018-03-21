using System;
using System.Management.Automation;
using MiniBlog.Contract;

namespace MiniBlog.Client.PowerShell
{
    /// <summary>
    /// Add-Article command-let
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "Article")]
    public class AddArticleCmdlet : BlogCmdletBase
    {
        /// <summary>
        /// Article content.
        /// </summary>
        [Parameter(Position = 1, Mandatory = true)]
        [Alias("c")]
        public string Content { get; set; }

        /// <summary>
        /// Article header.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        [Alias("h")]
        public string Header { get; set; }

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            Client.AddArticle(new ArticleDto()
            {
                Header = Header,
                Content = Content,
            });
            WriteVerbose($"Processing record \"{Header}\"");
        }
    }
}
