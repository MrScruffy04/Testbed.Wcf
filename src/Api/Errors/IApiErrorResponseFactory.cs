namespace AbstractInterfaces.Api.Errors
{
	using System;
	using System.Collections.Generic;
	using System.Net;

	public interface IApiErrorResponseFactory
	{
		HttpStatusCode GetStatusCode(Exception exception);

		IEnumerable<ErrorModel> GetMessages(Exception exception);
	}
}