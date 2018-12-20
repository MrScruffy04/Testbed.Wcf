namespace AbstractInterfaces.Api
{
	using System;
	using System.ServiceModel.Activation;

	public abstract class ApiServiceDefinition<T> : IApiServiceDefinition where T : ApiService
	{
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
	}
}
