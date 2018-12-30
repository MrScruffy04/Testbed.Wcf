namespace AbstractInterfaces.Api.Errors
{
	public sealed class NotAuthorizedException : ApiException
	{
		public NotAuthorizedException()
		{
		}

		public NotAuthorizedException(string message) : base(message)
		{
		}
	}
}
