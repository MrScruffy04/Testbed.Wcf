namespace AbstractInterfaces.Api.Xml
{
	using System;

	public static class XmlFormattingExtensions
	{
		public static string ToXmlText(this DateTime dateTime)
		{
			return dateTime
				.ToUniversalTime()
				.AddTicks(-(dateTime.Ticks % TimeSpan.TicksPerSecond))
				.ToString("yyyy-MM-ddTHH:mm:ssZ");
		}
	}
}
