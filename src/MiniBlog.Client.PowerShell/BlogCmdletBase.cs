using System;
using System.Management.Automation;
using System.ServiceModel;
using MiniBlog.ServiceClient.Integration;

namespace MiniBlog.Client.PowerShell
{
    /// <summary>
    /// Abstract command-let.
    /// </summary>
    public abstract class BlogCmdletBase : Cmdlet
    {
        /// <summary>
        /// Service client.
        /// </summary>
        protected BlogServiceClient Client;

        /// <summary>
        /// Service url.
        /// </summary>
        [Parameter]
        [Alias("s")]
        public string ServiceUrl { get; set; } = "http://localhost:49542/Services/BlogService.svc";

        /// <inheritdoc />
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            var binding = new BasicHttpBinding();
            Client = new BlogServiceClient(binding, new EndpointAddress(ServiceUrl));
            WriteVerbose($"Connecting to {ServiceUrl} ...");
        }

        /// <inheritdoc />
        protected override void EndProcessing()
        {
            base.EndProcessing();
            Client.Close();
            WriteVerbose($"Connection to {ServiceUrl} closed");
        }
    }
}
