namespace AbstractInterfaces.Api.Errors
{
	using System;

	public class ApiException : Exception
	{
		public ApiException()
		{
		}

		public ApiException(string message) : base(message)
		{
		}

		public ApiException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
