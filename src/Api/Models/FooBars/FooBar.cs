namespace AbstractInterfaces.Api.Models.FooBars
{
	using AbstractInterfaces.Api.Xml;
	using System;
	using System.Collections.Generic;
	using System.Xml.Serialization;

	[XmlRoot("foobar")]
	public class FooBar
	{
		[XmlElement("name")]
		public string Name { get; set; }

		[XmlElement("age")]
		public int Age { get; set; }

		[XmlElement("acceptedTerms")]
		public string AcceptedTermsXml
		{
			get { return AcceptedTerms.ToString().ToLowerInvariant(); }
			set { AcceptedTerms = value.ToBoolean(); }
		}
		[XmlIgnore]
		public bool AcceptedTerms { get; set; }

		[XmlElement("birthDate")]
		public string BirthDateXml
		{
			get { return BirthDate.ToXmlText(); }
			set { BirthDate = DateTime.Parse(value); }
		}
		[XmlIgnore]
		public DateTime BirthDate { get; set; }

		[XmlArray("interests"), XmlArrayItem("interest")]
		public List<string> Interests { get; set; }
	}
}
