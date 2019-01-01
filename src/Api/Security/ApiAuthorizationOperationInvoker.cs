namespace AbstractInterfaces.Api.Security
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.ServiceModel.Dispatcher;
	using System.Threading;

	public class ApiAuthorizationOperationInvoker : IOperationInvoker
	{
		private readonly IOperationInvoker _originalInvoker;
		private readonly MethodInfo _operationMethod;

		public ApiAuthorizationOperationInvoker(IOperationInvoker originalInvoker, MethodInfo operationMethod)
		{
			_originalInvoker = originalInvoker;
			_operationMethod = operationMethod;
		}

		public bool IsSynchronous => _originalInvoker.IsSynchronous;

		public object[] AllocateInputs()
		{
			return _originalInvoker.AllocateInputs();
		}

		public object Invoke(object instance, object[] inputs, out object[] outputs)
		{
			if (_operationMethod != null)
			{
				EnsureAccess(instance);
			}

			return _originalInvoker.Invoke(instance, inputs, out outputs);
		}

		public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
		{
			if (_operationMethod != null)
			{
				EnsureAccess(instance);
			}

			return _originalInvoker.InvokeBegin(instance, inputs, callback, state);
		}

		public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
		{
			return _originalInvoker.InvokeEnd(instance, out outputs, result);
		}

		private void EnsureAccess(object instance)
		{
			var principal = Thread.CurrentPrincipal;

			if (GetAuthorizeAttributes(_operationMethod).Any(att => !att.IsAuthorized(principal)))
			{
				throw new Errors.NotAuthorizedException();
			}
		}

		private static IEnumerable<AuthorizeAttribute> GetAuthorizeAttributes(MethodInfo methodInfo)
		{
			foreach (var att in methodInfo.DeclaringType.GetCustomAttributes<AuthorizeAttribute>())
			{
				yield return att;
			}

			foreach (var att in methodInfo.GetCustomAttributes<AuthorizeAttribute>())
			{
				yield return att;
			}
		}
	}
}
