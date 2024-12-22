using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace PTSL.GENERIC.Common.Helper;

public class TokenDecoder
{
    public static Claim? GetClaim(string accessToken, string claimType)
    {
        return new JwtSecurityToken(accessToken).Claims.FirstOrDefault(x => x.Type == claimType);
    }
}