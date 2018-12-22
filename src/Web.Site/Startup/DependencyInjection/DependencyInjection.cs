namespace AbstractInterfaces.Web.Startup.DependencyInjection
{
	using Autofac;

	public static class DependencyInjection
	{
		public static void Configure()
		{
			var builder = new ContainerBuilder();

			RegisterModulesAndTypes(builder);

			var container = builder.Build();

			SetDependencyResolver(container);
		}

		private static void RegisterModulesAndTypes(ContainerBuilder builder)
		{
			builder.RegisterModule<SiteModule>();

			RegisterApiModules(builder);
		}

		private static void RegisterApiModules(ContainerBuilder builder)
		{
			builder.RegisterModule<Api.FooBarsModule>();
		}

		private static void SetDependencyResolver(IContainer container)
		{
			Autofac.Integration.Wcf.AutofacHostFactory.Container = container;
		}
	}
}