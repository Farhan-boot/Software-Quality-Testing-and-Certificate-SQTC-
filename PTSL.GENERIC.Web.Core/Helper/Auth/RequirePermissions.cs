using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;

namespace PTSL.GENERIC.Web.Core.Helper.Auth;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequirePermissions : Attribute, IAuthorizationFilter
{
    private readonly string[] _permissions;
    public string RedirectTo { get; set; } = string.Empty;
    public bool IsJson = false;

    public RequirePermissions(params string[] permissions)
    {
        _permissions = permissions;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userRoleService = context.HttpContext.RequestServices.GetRequiredService<IUserRoleService>();

        var permissions = userRoleService.CurrentUserHasPermissions(_permissions);

        if (permissions.All(x => x.Value) == false)
        {
            context.HttpContext.Session.SetString("Message", "You do not have permission to perform this action");
            context.HttpContext.Session.SetString("executionState", ExecutionState.Failure.ToString());

            if (!string.IsNullOrEmpty(RedirectTo))
            {
                context.Result = RedirectToPage(RedirectTo);
            }
            else
            {
                if (IsJson)
                {
                    context.Result = new JsonResult(
                        new
                        {
                            Success = false,
                            Message = "You do not have permission to perform this action",
                        },
                        SerializerOption.Default
                    );
                }
                else
                {
                    var referrer = context.HttpContext.Request.Headers.Referer.ToString();
                    context.Result = new RedirectResult(referrer);
                }
            }
        }
    }

    public IActionResult RedirectToPage(string redirectTo)
    {
        var split = redirectTo.Split('/');

        return split.Length switch
        {
            2 => new RedirectToRouteResult(new RouteValueDictionary { { "action", split[1] }, { "controller", split[0] } }),
            _ => new RedirectToRouteResult(new RouteValueDictionary { { "action", "Index" }, { "controller", "Home" } })
        };
    }

    public IActionResult RedirectToBeneficiaryIndex()
    {
        return new RedirectToRouteResult(new RouteValueDictionary { { "action", "BeneficiaryIndex" }, { "controller", "Home" } });
    }
}

