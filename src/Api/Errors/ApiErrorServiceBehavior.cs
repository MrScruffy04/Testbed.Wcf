namespace AbstractInterfaces.Api.Errors
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ServiceModel;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Description;
	using System.ServiceModel.Dispatcher;

	public sealed class ApiErrorServiceBehavior : IServiceBehavior
	{
		private readonly IEnumerable<IErrorHandler> _errorHandlers;

		public ApiErrorServiceBehavior(IEnumerable<IErrorHandler> errorHandlers)
		{
			_errorHandlers = errorHandlers;
		}

		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
			{
				channelDispatcher.ErrorHandlers.Clear();

				foreach (var errorHandler in _errorHandlers)
				{
					channelDispatcher.ErrorHandlers.Add(errorHandler);
				}
			}
		}

		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}
	}
}
