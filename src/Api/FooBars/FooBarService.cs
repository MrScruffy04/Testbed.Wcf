namespace AbstractInterfaces.Api.FooBars
{
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
		public XElement GetDate()
		{
			return new XElement("root", DateTime.UtcNow);
		}
	}
}
