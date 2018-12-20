namespace AbstractInterfaces.Api
{
	using System.ServiceModel.Activation;

	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public abstract class ApiService
	{
		//TODO: Try to confine static dependencies (OperationContext, WebOperationContext, HttpContext, etc.) to this class or child services
	}
}
