using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Repositories.Models;
using WebApi.Utils.Attributes;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    [Authorize]
    [HttpGet]
    public ActionResult Func()
    {
        return Ok();
    }
}