using System;
using System.Management.Automation;
using System.ServiceModel;
using MiniBlog.ServiceClient.Integration;

namespace MiniBlog.Client.PowerShell
{
    [Cmdlet(VerbsCommon.Remove, "Article")]
    public class RemoveArticleCmdlet : BlogCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public int Id { get; set; }

        protected override void ProcessRecord()
        {
            Client.DeleteArticle(Id);
            WriteVerbose($"Processing record #{Id}");
        }
    }
}
