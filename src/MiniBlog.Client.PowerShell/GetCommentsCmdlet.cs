using System;
using System.Management.Automation;
using MiniBlog.Contract;

namespace MiniBlog.Client.PowerShell
{
    [Cmdlet(VerbsCommon.Get, "Comments")]
    [OutputType(typeof(CommentDto))]
    public class GetCommentsCmdlet : BlogCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public int Id { get; set; }

        protected override void ProcessRecord()
        {
            var article = Client.GetArticle(Id);
            WriteVerbose($"Processing record #{Id}");
            if (article.Comments?.Count > 0)
            {
                foreach (var comment in article.Comments)
                {
                    WriteObject(comment);
                }
            }
        }
    }
}
