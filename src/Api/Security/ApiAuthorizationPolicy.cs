namespace AbstractInterfaces.Api.Security
{
	using System;
	using System.Collections.Generic;
	using System.IdentityModel.Claims;
	using System.IdentityModel.Policy;
	using System.Linq;
	using System.Security.Principal;
	using System.ServiceModel;

	public sealed class ApiAuthorizationPolicy : IAuthorizationPolicy
	{
		public ClaimSet Issuer { get; } = ClaimSet.System;

		public string Id { get; } = Guid.NewGuid().ToString();

		public bool Evaluate(EvaluationContext evaluationContext, ref object state)
		{
			if (OperationContext.Current.IncomingMessageProperties["Principal"] is IPrincipal principal)
			{
				evaluationContext.Properties["Principal"] = principal;
				evaluationContext.Properties["Identities"] = new List<IIdentity> { principal.Identity, };

				var roleClaims = GetRoleClaims(principal as GenericPrincipal).ToList();

				evaluationContext.AddClaimSet(this, new DefaultClaimSet(roleClaims));
			}

			return true;
		}

		private IEnumerable<Claim> GetRoleClaims(GenericPrincipal principal)
		{
			return principal == null
				? Enumerable.Empty<Claim>()
				: principal.Claims.Select(c => new Claim(c.Type, c.Value, Rights.PossessProperty));
		}
	}
}
