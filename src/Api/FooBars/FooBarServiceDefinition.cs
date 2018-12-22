namespace AbstractInterfaces.Api.FooBars
{
	using System.ServiceModel.Activation;

	public sealed class FooBarServiceDefinition : ApiServiceDefinition<FooBarService>
	{
		private const string RoutePrefix = "FooBar";

		public FooBarServiceDefinition(ServiceHostFactory serviceHostFactory) : base(RoutePrefix, serviceHostFactory)
		{
		}
	}
}
