namespace AbstractInterfaces.Api.Errors
{
	using AbstractInterfaces.Api.Xml;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Dispatcher;
	using System.Web;
	using System.Xml;
	using System.Xml.Serialization;

	public sealed class ApiErrorHandler : IErrorHandler
	{
		private const string ResponseContentType = "application/xml";

		private readonly IApiErrorResponseFactory _errorResponseFactory;
		private readonly XmlSerializer _xmlSerializer;

		public ApiErrorHandler(IApiErrorResponseFactory errorResponseFactory)
		{
			_errorResponseFactory = errorResponseFactory;
			_xmlSerializer = new XmlSerializer(typeof(List<ErrorModel>), new XmlRootAttribute("errors"));
		}

		public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
		{
			fault = GetFault(version, error);

			SetResponse(error, fault);
		}

		private Message GetFault(MessageVersion version, Exception error)
		{
			var xml = SerializeError(error);

			var fault = string.IsNullOrEmpty(xml)
				? Message.CreateMessage(version, string.Empty)
				: Message.CreateMessage(version, string.Empty, new ErrorBodyWriter(xml));

			fault.Properties.Add(WebBodyFormatMessageProperty.Name, new WebBodyFormatMessageProperty(WebContentFormat.Xml));

			return fault;
		}

		private string SerializeError(Exception error)
		{
			var errorMessages = _errorResponseFactory.GetMessages(error).ToList();

			if (errorMessages.Count == 0)
			{
				return null;
			}

			return _xmlSerializer.SerializeToString(errorMessages);
		}

		private void SetResponse(Exception error, Message fault)
		{
			var statusCode = _errorResponseFactory.GetStatusCode(error);

			var response = new HttpResponseMessageProperty
			{
				StatusCode = statusCode,
				StatusDescription = GetStatusCodeDescription(statusCode),
			};

			response.Headers[HttpResponseHeader.ContentType] = ResponseContentType;

			if (statusCode == HttpStatusCode.Unauthorized)
			{
				response.Headers[HttpResponseHeader.WwwAuthenticate] = $"Basic realm=\"{ HttpContext.Current.Request.Url.AbsoluteUri }\"";
			}

			fault.Properties.Add(HttpResponseMessageProperty.Name, response);
		}

		public static string GetStatusCodeDescription(HttpStatusCode statusCode)
		{
			return Enum.GetName(typeof(HttpStatusCode), statusCode)
				?? Enum.GetName(typeof(HttpStatusCode), HttpStatusCode.InternalServerError);
		}

		public bool HandleError(Exception error)
		{
			var statusCode = _errorResponseFactory.GetStatusCode(error);

			if (statusCode == 0 || (int)statusCode >= 500)
			{
				//TODO: Log exception
				//Use ExceptionTelemetry to log exception
				//client.TrackException()
			}

			return true;  //  Meaning that the exception was "contained"
		}

		private class ErrorBodyWriter : BodyWriter
		{
			private readonly string _content;

			public ErrorBodyWriter(string content) : base(true)
			{
				_content = content;
			}

			protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
			{
				using (var sr = new System.IO.StringReader(_content))
				using (var xr = XmlReader.Create(sr))
				{
					writer.WriteNode(xr, true);
				}
			}
		}
	}
}
