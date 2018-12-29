namespace AbstractInterfaces.Web.Startup.ServiceHostConfiguration
{
	using System.Collections.Generic;
	using System.IdentityModel.Policy;
	using System.Linq;
	using System.ServiceModel;
	using System.ServiceModel.Description;

	public static class ServiceHostConfiguration
	{
		public static void Configure(ServiceHostBase serviceHost, ServiceAuthenticationManager authenticationManager, IEnumerable<IAuthorizationPolicy> authorizationPolicies)
		{
			SetAuthenticationManager(serviceHost, authenticationManager);

			SetAuthorizationPolicies(serviceHost, authorizationPolicies);
		}

		private static void SetAuthenticationManager(ServiceHostBase serviceHost, ServiceAuthenticationManager authenticationManager)
		{
			serviceHost.Authentication.ServiceAuthenticationManager = authenticationManager;
		}

		private static void SetAuthorizationPolicies(ServiceHostBase serviceHost, IEnumerable<IAuthorizationPolicy> authorizationPolicies)
		{
			serviceHost.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;

			serviceHost.Authorization.ExternalAuthorizationPolicies = authorizationPolicies.ToList().AsReadOnly();
		}
	}
}