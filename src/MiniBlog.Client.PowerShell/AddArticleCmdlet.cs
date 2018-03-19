using System;
using System.Management.Automation;
using MiniBlog.Contract;
using MiniBlog.ServiceClient.Integration;

namespace MiniBlog.Client.PowerShell
{
    [Cmdlet(VerbsCommon.Add, "Article")]
    public class AddArticleCmdlet : BlogCmdletBase
    {
        private BlogServiceClient client;

        [Parameter(Position = 1, Mandatory = true)]
        [Alias("c")]
        public string Content { get; set; }

        [Parameter(Position = 0, Mandatory = true)]
        [Alias("h")]
        public string Header { get; set; }

        protected override void ProcessRecord()
        {
            client.AddArticle(new ArticleDto()
            {
                Header = Header,
                Content = Content,
            });
            WriteVerbose($"Processing record \"{Header}\"");
        }
    }
}
