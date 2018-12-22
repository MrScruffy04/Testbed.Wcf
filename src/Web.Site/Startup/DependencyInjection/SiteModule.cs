namespace AbstractInterfaces.Web.Startup.DependencyInjection
{
	using Autofac;

	public sealed class SiteModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<Autofac.Integration.Wcf.AutofacWebServiceHostFactory>()
				.As<System.ServiceModel.Activation.ServiceHostFactory>()
				.SingleInstance();
		}
	}
}