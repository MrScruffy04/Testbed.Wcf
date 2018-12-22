namespace AbstractInterfaces.Api
{
	using AbstractInterfaces.Api.FooBars;
	using Autofac;

	public sealed class FooBarsModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<FooBarServiceDefinition>()
				.AsImplementedInterfaces()
				.SingleInstance();

			builder.RegisterType<FooBarService>()
				.SingleInstance();
		}
	}
}
