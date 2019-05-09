namespace AbstractInterfaces.Api.Xml
{
	public static class XmlParsingExtensions
	{
		public static bool ToBoolean(this string value)
		{
			switch (value.ToLowerInvariant())
			{
				case "true":
				case "1":
				case "on":
				case "yes":
					return true;

				default:
					return false;
			}
		}
	}
}
