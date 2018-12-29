namespace AbstractInterfaces.Api.Security
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Principal;

	public static class AuthorizationHeaderReader
	{
		private const string Prefix = "Basic ";

		public const string AlphaRole = "Alpha";
		private const string AlphaTag = "one";
		public const string BravoRole = "Bravo";
		private const string BravoTag = "two";

		public static IPrincipal FromAuthorizationHeader(string authorizationHeader)
		{
			var values = GetAuthorizationValues(authorizationHeader);

			var principal = values == null
				? GetAnonymousPrincipal()
				: GetPrincipal(values);

			return principal;
		}

		private static AuthorizationValues GetAuthorizationValues(string authorizationHeader)
		{
			if (string.IsNullOrEmpty(authorizationHeader)
				|| !authorizationHeader.StartsWith(Prefix, StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}

			var items = authorizationHeader.Substring(Prefix.Length).Split(new[] { ' ', }, StringSplitOptions.RemoveEmptyEntries);

			if (items.Length == 0)
			{
				return null;
			}

			return new AuthorizationValues(items[0], items.Skip(1));
		}

		private static IPrincipal GetAnonymousPrincipal()
		{
			return new GenericPrincipal(new GenericIdentity(string.Empty), new string[0]);
		}

		private static IPrincipal GetPrincipal(AuthorizationValues values)
		{
			return new GenericPrincipal(
				new GenericIdentity(values.IdentityName),
				values.Tags
					.Select(t => MapTagToRole(t))
					.Where(r => !string.IsNullOrEmpty(r))
					.ToArray());
		}

		private static string MapTagToRole(string tag)
		{
			switch (tag)
			{
				case AlphaTag:
					return AlphaRole;

				case BravoTag:
					return BravoRole;

				default:
					return null;
			}
		}



		private class AuthorizationValues
		{
			public AuthorizationValues(string identityName, IEnumerable<string> tags)
			{
				IdentityName = identityName;
				Tags = tags;
			}

			public string IdentityName { get; }

			public IEnumerable<string> Tags { get; }
		}
	}
}
