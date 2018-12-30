namespace AbstractInterfaces.Api.Xml
{
	using System.IO;
	using System.Text;
	using System.Xml;
	using System.Xml.Serialization;

	public static class XmlSerializerExtensions
	{
		public static string SerializeToString<T>(this XmlSerializer xmlSerializer, T value, bool indent = false, bool omitXmlDeclaration = true)
		{
			if (value == null)
			{
				return null;
			}

			var ns = new XmlSerializerNamespaces();
			ns.Add(string.Empty, string.Empty);

			var settings = new XmlWriterSettings()
			{
				Encoding = new UnicodeEncoding(false, false),
				Indent = indent,
				OmitXmlDeclaration = omitXmlDeclaration,
			};

			using (var sw = new StringWriter())
			{
				using (var xw = XmlWriter.Create(sw, settings))
				{
					xmlSerializer.Serialize(xw, value, ns);
				}

				return sw.ToString();
			}
		}

		public static T DeserializeFromString<T>(this XmlSerializer xmlSerializer, string xml)
		{
			if (string.IsNullOrEmpty(xml))
			{
				return default(T);
			}

			var settings = new XmlReaderSettings();

			using (var sr = new StringReader(xml))
			{
				using (var xr = XmlReader.Create(sr, settings))
				{
					return (T)xmlSerializer.Deserialize(xr);
				}
			}
		}
	}
}
