namespace AbstractInterfaces.Web.Startup.AppInsightsConfiguration
{
	using AbstractInterfaces.Api;
	using Microsoft.ApplicationInsights.Channel;
	using Microsoft.ApplicationInsights.DataContracts;
	using Microsoft.ApplicationInsights.Extensibility;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class RestfulTelemetryInitializer : ITelemetryInitializer
	{
		private readonly IEnumerable<IOpNameFactory> _opNameFactories;

		public RestfulTelemetryInitializer(IEnumerable<IOpNameFactory> opNameFactories)
		{
			_opNameFactories = opNameFactories ?? throw new ArgumentNullException(nameof(opNameFactories));
		}

		public void Initialize(ITelemetry telemetry)
		{
			if (telemetry is RequestTelemetry rt)
			{
				EvaluateRequest(rt);
			}
		}

		private void EvaluateRequest(RequestTelemetry requestTelemetry)
		{
			NormalizeOpName(requestTelemetry);
		}

		private void NormalizeOpName(RequestTelemetry requestTelemetry)
		{
			var opName = _opNameFactories
				.Select(factory => factory.GetOpName(requestTelemetry.Name))
				.FirstOrDefault(o => o != null);

			if (opName != null)
			{
				requestTelemetry.Name = opName.Path;
				requestTelemetry.Context.Operation.Name = opName.Path;

				foreach (var item in opName.Tokens)
				{
					requestTelemetry.Properties.Add(item);
				}
			}
		}
	}
}