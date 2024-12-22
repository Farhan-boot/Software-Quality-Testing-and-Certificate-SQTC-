using Microsoft.AspNetCore.Authorization;

namespace PTSL.GENERIC.Api.Helpers.Authorize;

public class RoleClaimsRequirement : IAuthorizationRequirement
{
    public string ClaimType { get; }
    public string ClaimValue { get; }

    public RoleClaimsRequirement(string claimType, string claimValue)
    {
        ClaimType = claimType;
        ClaimValue = claimValue;
    }
}
