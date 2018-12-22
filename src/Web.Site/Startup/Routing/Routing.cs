namespace AbstractInterfaces.Web.Startup.Routing
{
	using Autofac;
	using System;
	using System.Collections.Generic;
	using System.Web.Routing;

	public static class Routing
	{
		public static void Configure()
		{
			var container = GetDependencyInjectionResolver();

			RegisterRoutes(container.Resolve<IEnumerable<Api.IApiServiceDefinition>>());
		}

		private static IContainer GetDependencyInjectionResolver()
		{
			//  Ideally, we would not have a reference to Autofac in this class, but there's no extensibility point just for DI
			var container = Autofac.Integration.Wcf.AutofacHostFactory.Container as IContainer;

			return container ?? throw new InvalidOperationException("Dependency Injection Resolver not yet defined");
		}

		private static void RegisterRoutes(IEnumerable<Api.IApiServiceDefinition> serviceDefinitions)
		{
			foreach (var d in serviceDefinitions)
			{
				RouteTable.Routes.Add(d.GetRoute());
			}
		}
	}
}