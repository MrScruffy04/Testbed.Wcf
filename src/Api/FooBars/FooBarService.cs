namespace AbstractInterfaces.Api.FooBars
{
	using AbstractInterfaces.Api.Errors;
	using AbstractInterfaces.Api.Security;
	using System;
	using System.ServiceModel;
	using System.ServiceModel.Web;
	using System.Xml.Linq;

	[ServiceContract]
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public sealed class FooBarService : ApiService
	{
		public FooBarService()
		{
			System.Diagnostics.Debug.WriteLine("FooBarService.ctor() invoked");
		}

		[WebGet(UriTemplate = "today")]
		[Authorize(Roles = "Alpha, Bravo")]
		public XElement GetDate()
		{
			return new XElement("root", DateTime.UtcNow);
		}

		[WebGet(UriTemplate = "tomorrow")]
		public XElement GetUnkownResource()
		{
			throw new ResourceNotFoundException();
		}

		[WebGet(UriTemplate = "unhandled")]
		public XElement GetUnhandledException()
		{
			throw new Exception("oops");
		}

		[WebGet(UriTemplate = "{key}")]
		public XElement GetResource(string key)
		{
			var resource = GetOwnedResource(key);

			return new XElement("resource", resource);
		}

		private string GetOwnedResource(string key)
		{
			EnsureOwnership(key);

			//  Retrieve a resource based on the key
			return key;
		}

		private void EnsureOwnership(string key /*object resource*/)
		{
			//  This can be done by evaluating the key (preferred) or the resource
			if (GetPrincipal().Identity.Name != key)
			{
				throw new NotAuthorizedException(); //  Use base type for all application errors
			}
		}
	}
}
