using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repositories.Enums;
using Repositories.Models;

namespace WebApi.Utils.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
sealed class ModeratorAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // UserAccount mora da postoji posto se mod check ne bi desio ako authorisation check ne prodje
        if ((context.HttpContext.Items["User"] as UserAccount).Permissions != UserPermissions.Moderator)
            context.Result = new JsonResult(new { message = "Requires moderator privileges" }) { StatusCode = StatusCodes.Status403Forbidden };
    }
}
