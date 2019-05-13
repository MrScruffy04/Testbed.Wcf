namespace AbstractInterfaces.Api
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.ServiceModel.Activation;
	using System.Text;
	using System.Text.RegularExpressions;

	public abstract class ApiServiceDefinition<T> : IApiServiceDefinition, IOpNameFactory where T : ApiService
	{
		private static readonly Regex OpNameRegex = new Regex(@"^(?<method>\w+)\s(?<endpoint>.+)$", RegexOptions.Compiled);

		private readonly ServiceHostFactory _serviceHostFactory;

		public ApiServiceDefinition(string prefix, ServiceHostFactory serviceHostFactory)
		{
			Prefix = prefix ?? throw new ArgumentNullException(nameof(prefix));
			_serviceHostFactory = serviceHostFactory ?? throw new ArgumentNullException(nameof(serviceHostFactory));
		}

		protected string Prefix { get; private set; }

		public ServiceRoute GetRoute()
		{
			return new ServiceRoute(Prefix, _serviceHostFactory, typeof(T));
		}

		public virtual OpName GetOpName(string denormalized)
		{
			if (string.IsNullOrEmpty(denormalized))
			{
				return null;
			}

			return GetOpNameCore(denormalized);
		}

		private OpName GetOpNameCore(string denormalized)
		{
			var match = OpNameRegex.Match(denormalized);

			if (match.Success)
			{
				var (path, tokens) = GetNormalizedEndpoint(match.Groups["endpoint"].Value);

				if (!string.IsNullOrEmpty(path))
				{
					return new OpName
					{
						Path = $"{match.Groups["method"].Value} {path}",
						Tokens = tokens,
					};
				}
			}

			return null;
		}

		private (string, Dictionary<string, string>) GetNormalizedEndpoint(string denormalizedEndpoint)
		{
			var sb = new StringBuilder();
			var tokens = new Dictionary<string, string>();

			var arr = denormalizedEndpoint.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

			if (arr.Length > 0 
				&& Prefix.Equals(arr[0], StringComparison.OrdinalIgnoreCase))
			{
				sb.Append("/");
				sb.Append(Prefix);

				if (arr.Length > 1)
				{
					sb.Append("/{id}");

					tokens.Add("id", arr[1]);

					foreach (var sub in arr.Skip(2))
					{
						sb.Append("/");
						sb.Append(sub);
					}
				}
			}

			return (sb.ToString(), tokens);
		}
	}
}
