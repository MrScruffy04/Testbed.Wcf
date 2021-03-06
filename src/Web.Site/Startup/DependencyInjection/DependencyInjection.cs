﻿namespace AbstractInterfaces.Web.Startup.DependencyInjection
{
	using Autofac;
	using System.Collections.Generic;
	using System.IdentityModel.Policy;
	using System.ServiceModel.Description;

	public static class DependencyInjection
	{
		public static void Configure()
		{
			var builder = new ContainerBuilder();

			RegisterModulesAndTypes(builder);

			var container = builder.Build();

			SetDependencyResolver(container);

			CustomizeServiceHostFactory(container);
		}

		private static void RegisterModulesAndTypes(ContainerBuilder builder)
		{
			builder.RegisterModule<SiteModule>();

			RegisterApiModules(builder);
		}

		private static void RegisterApiModules(ContainerBuilder builder)
		{
			builder.RegisterModule<Api.ErrorModule>();
			builder.RegisterModule<Api.FooBarsModule>();
			builder.RegisterModule<Api.SecurityModule>();
		}

		private static void SetDependencyResolver(IContainer container)
		{
			Autofac.Integration.Wcf.AutofacHostFactory.Container = container;
		}

		private static void CustomizeServiceHostFactory(IContainer container)
		{
			Autofac.Integration.Wcf.AutofacHostFactory.HostConfigurationAction = serviceHost =>
			{
				var authenticationManager = container.Resolve<System.ServiceModel.ServiceAuthenticationManager>();

				var authorizationPolicies = container.Resolve<IEnumerable<IAuthorizationPolicy>>();

				var authorizationInvokerBehavior = container.ResolveNamed<IOperationBehavior>(Api.SecurityModule.AuthorizationCheck);

				var errorBehavior = container.ResolveNamed<IServiceBehavior>(Api.ErrorModule.ErrorBehavior);

				ServiceHostConfiguration.ServiceHostConfiguration.Configure(serviceHost, authenticationManager, authorizationPolicies, authorizationInvokerBehavior, errorBehavior);
			};
		}
	}
}