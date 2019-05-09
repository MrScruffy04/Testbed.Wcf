namespace AbstractInterfaces.Api.FooBars
{
	using AbstractInterfaces.Api.Errors;
	using AbstractInterfaces.Api.Models.FooBars;
	using AbstractInterfaces.Api.Security;
	using System;
	using System.Collections.Generic;
	using System.ServiceModel;
	using System.ServiceModel.Web;
	using System.Xml.Linq;

	[ServiceContract]
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	[XmlSerializerFormat]
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
		public FooBar GetResource(string key)
		{
			var resource = GetOwnedResource(key);

			return GetOwnedResource(key);
		}

		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "{key}")]
		//public void UpdateResource(string key, FooBar resource)
		public void UpdateResource(string key, FooBar resource)
		{
			EnsureOwnership(key);

			//  Call persistence mechanism
		}

		private FooBar GetOwnedResource(string key)
		{
			EnsureOwnership(key);

			//  Retrieve a resource based on the key
			return new FooBar { Name = key, Age = 42, AcceptedTerms = true, BirthDate = DateTime.Parse("1982-01-08T00:04:00Z").ToUniversalTime(), Interests = new List<string> { "programming", "video games", "music", } };
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
