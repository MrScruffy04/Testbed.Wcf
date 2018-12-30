namespace AbstractInterfaces.Api
{
	using System.Security.Principal;
	using System.ServiceModel;
	using System.ServiceModel.Activation;

	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public abstract class ApiService
	{
		//TODO: Try to confine static dependencies (OperationContext, WebOperationContext, HttpContext, etc.) to this class or child services

		protected IPrincipal GetPrincipal()
		{
			return OperationContext.Current.IncomingMessageProperties.Security.ServiceSecurityContext.AuthorizationContext.Properties.TryGetValue("Principal", out object obj)
				? obj as IPrincipal
				: null;
		}
	}
}
