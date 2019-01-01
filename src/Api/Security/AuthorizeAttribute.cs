namespace AbstractInterfaces.Api.Security
{
	using System;
	using System.Linq;
	using System.Security.Principal;
	using System.Threading;

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class AuthorizeAttribute : Attribute
	{
		private string _roles;
		private string _identityNames;
		private string[] _rolesSplit = new string[0];
		private string[] _identityNamesSplit = new string[0];

		public string Roles
		{
			get
			{
				return _roles;
			}
			set
			{
				_roles = value;
				_rolesSplit = SplitString(_roles);
			}
		}

		public string IdentityNames
		{
			get
			{
				return _identityNames;
			}
			set
			{
				_identityNames = value;
				_identityNamesSplit = SplitString(_identityNames);
			}
		}

		private static string[] SplitString(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return new string[0];
			}

			var collection = value.Split(',')
				.Select(s => s.Trim())
				.Where(s => !string.IsNullOrEmpty(s));

			return collection.ToArray();
		}

		public bool IsAuthenticated { get; set; } = true;

		public bool IsAuthorized(IPrincipal principal = null)
		{
			principal = principal ?? Thread.CurrentPrincipal;

			if (IsAuthenticated && !principal.Identity.IsAuthenticated)
			{
				return false;
			}

			if (_identityNamesSplit.Any() && !_identityNamesSplit.Any(name => principal.Identity.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
			{
				return false;
			}

			if (_rolesSplit.Any() && !_rolesSplit.Any(role => principal.IsInRole(role)))
			{
				return false;
			}

			return true;
		}
	}
}
