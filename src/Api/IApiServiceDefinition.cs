namespace AbstractInterfaces.Api
{
	using System.ServiceModel.Activation;

	public interface IApiServiceDefinition
	{
		ServiceRoute GetRoute();
	}
}
