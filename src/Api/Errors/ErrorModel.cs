namespace AbstractInterfaces.Api.Errors
{
	using System.Xml.Serialization;

	[XmlType("error")]
	public sealed class ErrorModel
	{
		[XmlText]
		public string Message { get; set; }
	}
}
