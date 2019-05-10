﻿namespace AbstractInterfaces.Web.Startup.AppInsightsConfiguration
{
	using Microsoft.ApplicationInsights.Extensibility;
	using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;
	using System.Configuration;

	public static class AppInsightsConfiguration
	{
		public static void Configure()
		{
			var config = TelemetryConfiguration.Active;

			config.InstrumentationKey = GetInstrumentationKey();

			AddQuickPulseProcessor(config, GetQuickPulseApiKey());
		}

		private static string GetInstrumentationKey()
		{
			return ConfigurationManager.AppSettings["ApplicationInsights:InstrumentationKey"];
		}

		private static string GetQuickPulseApiKey()
		{
			return ConfigurationManager.AppSettings["ApplicationInsights:ApiKeys:QuickPulse"];
		}

		private static void AddQuickPulseProcessor(TelemetryConfiguration config, string apiKey)
		{
			QuickPulseTelemetryProcessor processor = null;

			config.TelemetryProcessorChainBuilder
				.Use(next =>
				{
					processor = new QuickPulseTelemetryProcessor(next);

					return processor;
				})
				.Build();

			var module = new QuickPulseTelemetryModule
			{
				AuthenticationApiKey = apiKey,
			};

			module.Initialize(config);
			module.RegisterTelemetryProcessor(processor);

			foreach (var tp in config.TelemetryProcessors)
			{
				if (tp is ITelemetryModule tm)
				{
					tm.Initialize(config);
				}
			}
		}
	}
}