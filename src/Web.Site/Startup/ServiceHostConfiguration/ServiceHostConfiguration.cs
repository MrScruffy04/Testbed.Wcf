namespace AbstractInterfaces.Web.Startup.ServiceHostConfiguration
{
	using System.Collections.Generic;
	using System.IdentityModel.Policy;
	using System.Linq;
	using System.ServiceModel;
	using System.ServiceModel.Description;

	public static class ServiceHostConfiguration
	{
		public static void Configure(ServiceHostBase serviceHost, ServiceAuthenticationManager authenticationManager, IEnumerable<IAuthorizationPolicy> authorizationPolicies, IOperationBehavior authorizationInvokerBehavior, IServiceBehavior errorBehavior)
		{
			SetAuthentication(serviceHost, authenticationManager, authorizationPolicies);

			SetAuthorizationInvokerBehavior(serviceHost, authorizationInvokerBehavior);

			SetErrorHandlers(serviceHost, errorBehavior);
		}

		private static void SetAuthentication(ServiceHostBase serviceHost, ServiceAuthenticationManager authenticationManager, IEnumerable<IAuthorizationPolicy> authorizationPolicies)
		{
			serviceHost.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;

			serviceHost.Authentication.ServiceAuthenticationManager = authenticationManager;

			serviceHost.Authorization.ExternalAuthorizationPolicies = authorizationPolicies.ToList().AsReadOnly();
		}

		private static void SetAuthorizationInvokerBehavior(ServiceHostBase serviceHost, IOperationBehavior authorizationInvokerBehavior)
		{
			serviceHost.Opening += (sender, args) =>
			{
				foreach (var ep in serviceHost.Description.Endpoints)
				{
					foreach (var od in ep.Contract.Operations)
					{
						od.OperationBehaviors.Add(authorizationInvokerBehavior);
					}
				}
			};
		}

		private static void SetErrorHandlers(ServiceHostBase serviceHost, IServiceBehavior errorBehavior)
		{
			serviceHost.Description.Behaviors.Add(errorBehavior);
		}
	}
}