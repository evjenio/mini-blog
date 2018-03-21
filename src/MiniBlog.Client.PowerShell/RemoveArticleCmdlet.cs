using System;
using System.Management.Automation;

namespace MiniBlog.Client.PowerShell
{
    /// <summary>
    /// Remove-Article command-let.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "Article")]
    public class RemoveArticleCmdlet : BlogCmdletBase
    {
        /// <summary>
        /// Id.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public int Id { get; set; }

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            Client.DeleteArticle(Id);
            WriteVerbose($"Processing record #{Id}");
        }
    }
}
