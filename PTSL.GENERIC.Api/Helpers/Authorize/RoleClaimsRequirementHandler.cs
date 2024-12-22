using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;

using PTSL.GENERIC.Business.TokenHelper;
using PTSL.GENERIC.Service.Services.Interface;

namespace PTSL.GENERIC.Api.Helpers.Authorize;

public class RoleClaimsRequirementHandler : AuthorizationHandler<RoleClaimsRequirement>
{
    private readonly IUserRoleService _userRoleService;

    public RoleClaimsRequirementHandler(IUserRoleService userRoleService)
    {
        _userRoleService = userRoleService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleClaimsRequirement requirement)
    {
        var userIdString = context.User.FindFirst(CustomClaimTypes.UserId)?.Value ?? throw new Exception("User id not found");
        _ = long.TryParse(userIdString, out var userId);

        var hasPermission = await _userRoleService.UserHasPermission(
            userId,
            new List<string>()
            {
                requirement.ClaimValue,
            });
        if (hasPermission)
        {
            context.Succeed(requirement);
            return;
        }

        context.Fail();
    }
}
