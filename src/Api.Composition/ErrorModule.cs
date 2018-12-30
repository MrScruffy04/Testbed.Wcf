namespace AbstractInterfaces.Api
{
	using AbstractInterfaces.Api.Errors;
	using Autofac;
	using System.ServiceModel.Description;

	public sealed class ErrorModule : Module
	{
		public const string ErrorBehavior = "ErrorBehavior";

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<ApiErrorServiceBehavior>()
				.Named<IServiceBehavior>(ErrorBehavior)
				.SingleInstance();

			builder.RegisterType<ApiErrorHandler>()
				.AsImplementedInterfaces()
				.SingleInstance();

			builder.RegisterType<ApiErrorResponseFactory>()
				.AsImplementedInterfaces()
				.SingleInstance();
		}
	}
}
