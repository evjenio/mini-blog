using System;
using System.Management.Automation;
using System.ServiceModel;
using MiniBlog.ServiceClient.Integration;

namespace MiniBlog.Client.PowerShell
{
    public abstract class BlogCmdletBase : Cmdlet
    {
        protected BlogServiceClient Client;

        [Parameter]
        [Alias("s")]
        public string ServiceUrl { get; set; } = "http://localhost:49542/Services/BlogService.svc";

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            var binding = new BasicHttpBinding();
            Client = new BlogServiceClient(binding, new EndpointAddress(ServiceUrl));
            WriteVerbose($"Connecting to {ServiceUrl} ...");
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
            Client.Close();
            WriteVerbose($"Connection to {ServiceUrl} closed");
        }
    }
}
