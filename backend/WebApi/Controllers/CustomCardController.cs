using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Repositories.Models;
using WebApi.Utils.Attributes;
using Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomCardController: ControllerBase
{
    private readonly ICustomCardService cardService;

    public CustomCardController(ICustomCardService service) {
        cardService = service;
    }

    
}
