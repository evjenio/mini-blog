using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Autofac;
using Autofac.Integration.Wcf;

namespace MiniBlog.WCF.ErrorHandling
{
    /// <summary>
    /// Global Error Behavior Attribute
    /// </summary>
    public class GlobalErrorBehaviorAttribute : Attribute, IServiceBehavior
    {
        private readonly Type errorHandlerType;

        /// <summary>
        /// Dependency injection to dynamically inject error handler
        /// if we have multiple global error handlers
        /// </summary>
        /// <param name="errorHandlerType">Type of handler</param>
        public GlobalErrorBehaviorAttribute(Type errorHandlerType)
        {
            this.errorHandlerType = errorHandlerType;
        }

        #region IServiceBehavior Members

        void IServiceBehavior.Validate(ServiceDescription description,
            ServiceHostBase serviceHostBase)
        {
        }

        void IServiceBehavior.AddBindingParameters(ServiceDescription description,
            ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection parameters)
        {
        }

        /// <summary>
        /// Registering the instance of global error handler in
        /// dispatch behavior of the service
        /// </summary>
        /// <param name="description">description</param>
        /// <param name="serviceHostBase">serviceHostBase</param>
        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
        {
            IErrorHandler errorHandler = (IErrorHandler)AutofacHostFactory.Container.Resolve(errorHandlerType);

            foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher channelDispatcher = channelDispatcherBase as ChannelDispatcher;
                channelDispatcher.ErrorHandlers.Add(errorHandler);
            }
        }

        #endregion IServiceBehavior Members
    }
}
