namespace AbstractInterfaces.Web.Startup.AppInsightsConfiguration
{
	using Microsoft.ApplicationInsights.Extensibility;
	using System.Configuration;

	public static class AppInsightsConfiguration
	{
		public static void Configure()
		{
			var config = TelemetryConfiguration.Active;

			config.InstrumentationKey = GetInstrumentationKey();
		}

		private static string GetInstrumentationKey()
		{
			return ConfigurationManager.AppSettings["ApplicationInsights:InstrumentationKey"];
		}
	}
}