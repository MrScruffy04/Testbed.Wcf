namespace AbstractInterfaces.Api
{
	using System;
	using System.Collections.Generic;

	public class OpName
	{
		public OpName()
		{
		}

		public OpName(string path)
		{
			Path = string.IsNullOrEmpty(path)
				? throw new ArgumentException("Path cannot be null or empty", nameof(path))
				: path;

			Tokens = new Dictionary<string, string>();
		}

		public string Path { get; set; }

		public IDictionary<string, string> Tokens { get; set; }
	}
}
