namespace AbstractInterfaces.Api.Security
{
	using System.ServiceModel.Channels;
	using System.ServiceModel.Description;
	using System.ServiceModel.Dispatcher;

	public class ApiAuthorizationOperationBehavior : IOperationBehavior
	{
		public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
		{
		}

		public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
		{
		}

		public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
		{
			var methodInfo = dispatchOperation.Invoker.IsSynchronous
				? operationDescription.SyncMethod
				: operationDescription.BeginMethod;

			dispatchOperation.Invoker = new ApiAuthorizationOperationInvoker(dispatchOperation.Invoker, methodInfo);
		}

		public void Validate(OperationDescription operationDescription)
		{
		}
	}
}
