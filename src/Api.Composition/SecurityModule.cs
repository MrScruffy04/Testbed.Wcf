namespace AbstractInterfaces.Api
{
	using AbstractInterfaces.Api.Security;
	using Autofac;
	using System.ServiceModel;

	public sealed class SecurityModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<ApiServiceAuthenticationManager>()
				.As<ServiceAuthenticationManager>()
				.SingleInstance();

			builder.RegisterType<ApiAuthorizationPolicy>()
				.AsImplementedInterfaces()
				.SingleInstance();
		}
	}
}
