using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace CazuelaChapina.API.Middleware;

public class PermissionAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _module;
    private readonly string _action;

    public PermissionAuthorizeAttribute(string module, string action)
    {
        _module = module;
        _action = action;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (user?.Identity?.IsAuthenticated != true)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var permissionsClaim = user.FindFirst("permissions")?.Value;
        if (permissionsClaim is null)
        {
            context.Result = new ForbidResult();
            return;
        }

        try
        {
            var permissions = JsonSerializer.Deserialize<IEnumerable<PermissionClaim>>(permissionsClaim);
            var hasPermission = permissions?.Any(p =>
                p.module.Equals(_module, StringComparison.OrdinalIgnoreCase) &&
                p.actions.Contains(_action)) ?? false;

            if (!hasPermission)
                context.Result = new ForbidResult();
        }
        catch
        {
            context.Result = new ForbidResult();
        }
    }

    private record PermissionClaim(string module, IEnumerable<string> actions);
}
