using Microsoft.AspNetCore.Http;

namespace PTSL.GENERIC.Api.Helpers.Extensions;

public static class HttpContextExtensions
{
    public static long GetCurrentUserId(this HttpContext context)
    {
        var userIdJwt = context.User.FindFirst("UserId")?.Value;
        _ = long.TryParse(userIdJwt, out var userId);

        return userId;
    }
}
