namespace AbstractInterfaces.Web
{
	using AbstractInterfaces.Api;
	using Autofac;
	using System;
	using System.Collections.Generic;
	using System.Web;

	public class Global : HttpApplication
	{

		protected void Application_Start(object sender, EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("Global.Application_Start() invoked");

			Startup.DependencyInjection.DependencyInjection.Configure();

			Startup.AppInsightsConfiguration.AppInsightsConfiguration.Configure(GetOpNameFactories());

			Startup.Routing.Routing.Configure();
		}

		private IEnumerable<IOpNameFactory> GetOpNameFactories()
		{
			return Autofac.Integration.Wcf.AutofacHostFactory.Container.Resolve<IEnumerable<IOpNameFactory>>();
		}

		protected void Session_Start(object sender, EventArgs e)
		{
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{
		}

		protected void Application_Error(object sender, EventArgs e)
		{
		}

		protected void Session_End(object sender, EventArgs e)
		{
		}

		protected void Application_End(object sender, EventArgs e)
		{
		}
	}
}