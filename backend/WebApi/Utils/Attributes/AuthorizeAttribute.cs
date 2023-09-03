using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Utils.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
sealed class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.Items["User"] is null) 
            context.Result = new JsonResult(new { message = "Not authorized!" }) { StatusCode = StatusCodes.Status401Unauthorized };
        
        // HttpContext.Items["User"] ce biti mesto gde bindujem user acc nakon sto validifikujem jwt webtoken iz headera
        // u slucaju da nije validan token, vrednost ce biti null sto znaci da korisnik nije ulogovan
    }
}
