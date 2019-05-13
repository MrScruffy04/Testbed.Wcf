namespace AbstractInterfaces.Api.FooBars
{
	using System.ServiceModel.Activation;

	public sealed class FooBarServiceDefinition : ApiServiceDefinition<FooBarService>
	{
		private const string RoutePrefix = "FooBar";

		public FooBarServiceDefinition(ServiceHostFactory serviceHostFactory) : base(RoutePrefix, serviceHostFactory)
		{
		}

		public override OpName GetOpName(string denormalized)
		{
			var opName = base.GetOpName(denormalized);

			if (opName != null)
			{
				switch (opName.Tokens?["id"]?.ToLowerInvariant())
				{
					case "today":
						opName = new OpName($"GET /{RoutePrefix}/today");
						break;

					case "tomorrow":
						opName = new OpName($"GET /{RoutePrefix}/tomorrow");
						break;

					case "unhandled":
						opName = new OpName($"GET /{RoutePrefix}/unhandled");
						break;
				}
			}

			return opName;
		}
	}
}
