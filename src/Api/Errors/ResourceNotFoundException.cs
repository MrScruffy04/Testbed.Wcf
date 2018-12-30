namespace AbstractInterfaces.Api.Errors
{
	public sealed class ResourceNotFoundException : ApiException
	{
		public ResourceNotFoundException()
		{
		}

		public ResourceNotFoundException(string message) : base(message)
		{
		}
	}
}
