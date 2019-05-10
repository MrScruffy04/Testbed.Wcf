namespace AbstractInterfaces.Web
{
	using System;
	using System.Web;

	public class Global : HttpApplication
	{

		protected void Application_Start(object sender, EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("Global.Application_Start() invoked");

			Startup.AppInsightsConfiguration.AppInsightsConfiguration.Configure();

			Startup.DependencyInjection.DependencyInjection.Configure();

			Startup.Routing.Routing.Configure();
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