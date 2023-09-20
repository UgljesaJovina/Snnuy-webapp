using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Repositories.Models;
using WebApi.Utils.Attributes;
using Services.DTOs;
using Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService service) {
        userService = service;
    }
}
