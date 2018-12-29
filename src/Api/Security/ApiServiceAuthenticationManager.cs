namespace AbstractInterfaces.Api.Security
{
	using System;
	using System.Collections.ObjectModel;
	using System.IdentityModel.Policy;
	using System.Net;
	using System.ServiceModel;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Web;

	public sealed class ApiServiceAuthenticationManager : ServiceAuthenticationManager
	{
		public override ReadOnlyCollection<IAuthorizationPolicy> Authenticate(ReadOnlyCollection<IAuthorizationPolicy> authPolicy, Uri listenUri, ref Message message)
		{
			var principal = AuthorizationHeaderReader.FromAuthorizationHeader(GetAuthorizationHeader());

			message.Properties["Principal"] = principal;

			return authPolicy;
		}

		private static string GetAuthorizationHeader()
		{
			return WebOperationContext.Current?.IncomingRequest?.Headers?[HttpRequestHeader.Authorization] ?? string.Empty;
		}
	}
}
