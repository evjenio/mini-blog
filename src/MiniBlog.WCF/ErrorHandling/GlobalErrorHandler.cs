using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using MiniBlog.Contract;
using Serilog;

namespace MiniBlog.WCF.ErrorHandling
{
    /// <summary>
    /// Global Error Handler
    /// </summary>
    public class GlobalErrorHandler : IErrorHandler
    {
        private readonly ILogger logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger">Logger</param>
        public GlobalErrorHandler(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// The method that's get invoked if any unhandled exception raised in service
        /// Here you can do what ever logic you would like to.
        /// For example logging the exception details
        /// Here the return value indicates that the exception was handled or not
        /// Return true to stop exception propagation and system considers
        /// that the exception was handled properly
        /// else return false to abort the session
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool HandleError(Exception error)
        {
            logger.Error(error, "Unhandled exception occurred");
            return true;
        }

        /// <summary>
        /// If you want to communicate the exception details to the service client
        /// as proper fault message
        /// here is the place to do it
        /// If we want to suppress the communication about the exception,
        /// set fault to null
        /// </summary>
        /// <param name="error"></param>
        /// <param name="version"></param>
        /// <param name="fault"></param>
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            var exception = new FaultException<ServiceFaultMessage>(
                new ServiceFaultMessage("Unhandled exception occurred. See logs for details"),
                new FaultReason("Unhandled exception"));
            MessageFault messageFault = exception.CreateMessageFault();
            fault = Message.CreateMessage(version, messageFault, exception.Action);
        }
    }
}
