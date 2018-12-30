namespace AbstractInterfaces.Api.Errors
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net;

	public sealed class ApiErrorResponseFactory : IApiErrorResponseFactory
	{
		public HttpStatusCode GetStatusCode(Exception exception)
		{
			if (exception is ResourceNotFoundException)
			{
				return HttpStatusCode.NotFound;
			}

			if (exception is NotAuthorizedException)
			{
				return HttpStatusCode.Unauthorized;
			}

			return HttpStatusCode.InternalServerError;
		}

		public IEnumerable<ErrorModel> GetMessages(Exception exception)
		{
			if (exception is AggregateException aggregateException)
			{
				var models = aggregateException.InnerExceptions.SelectMany(ex => GetMessages(ex));

				foreach (var model in models)
				{
					yield return model;
				}

				yield break;
			}

			string message = null;

			if (exception is ApiException apiException)
			{
				message = GetMessage(apiException);
			}
			else
			{
				message = exception.Message;
			}

			if (!string.IsNullOrEmpty(message))
			{
				yield return new ErrorModel { Message = message, };
			}
		}

		private static string GetMessage(ApiException apiException)
		{
			if (apiException is ResourceNotFoundException || apiException is NotAuthorizedException)
			{
				return null;
			}

			return apiException.Message;
		}
	}
}
