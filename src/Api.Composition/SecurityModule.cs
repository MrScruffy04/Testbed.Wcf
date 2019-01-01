namespace AbstractInterfaces.Api
{
	using AbstractInterfaces.Api.Security;
	using Autofac;
	using System.ServiceModel;
	using System.ServiceModel.Description;

	public sealed class SecurityModule : Module
	{
		public const string AuthorizationCheck = "AuthorizationCheck";

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<ApiServiceAuthenticationManager>()
				.As<ServiceAuthenticationManager>()
				.SingleInstance();

			builder.RegisterType<ApiAuthorizationPolicy>()
				.AsImplementedInterfaces()
				.SingleInstance();

			builder.RegisterType<ApiAuthorizationOperationBehavior>()
				.Named<IOperationBehavior>(AuthorizationCheck)
				.SingleInstance();
		}
	}
}
