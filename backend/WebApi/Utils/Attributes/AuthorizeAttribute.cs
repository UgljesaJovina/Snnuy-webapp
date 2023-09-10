using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repositories.Enums;
using Repositories.Models;

namespace WebApi.Utils.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
sealed class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly UserPermissions permissions = UserPermissions.None;

    public AuthorizeAttribute() { }

    public AuthorizeAttribute(UserPermissions _permissions) {
        permissions = _permissions;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        UserAccount? account = context.HttpContext.Items["User"] as UserAccount;

        if (account is null) {
            context.Result = new JsonResult(new { message = "You have to be logged in!" }) { StatusCode = StatusCodes.Status401Unauthorized };
            return;
        } 
        
        if (permissions != 0) {
            if ((account.Permissions & permissions) == 0)
                context.Result = new JsonResult(new { message = "You lack permissions for this action!" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        // HttpContext.Items["User"] ce biti mesto gde bindujem user acc nakon sto validifikujem jwt webtoken iz headera
        // u slucaju da nije validan token, vrednost ce biti null sto znaci da korisnik nije ulogovan
    }
}
