using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Repositories.Models;
using WebApi.Utils.Attributes;
using Services.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    [HttpPost]
    public ActionResult Func([FromBody] UpdateAccountRequest user)
    {
        Console.WriteLine($"name: {user.UserName}, Id: {user.Id}, Permissions: {user.Permissions}, password: {user.Password}");
        return Ok(user);
    }
}
