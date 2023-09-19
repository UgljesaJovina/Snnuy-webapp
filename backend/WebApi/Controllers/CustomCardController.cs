using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Repositories.Models;
using WebApi.Utils.Attributes;
using Services.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomCardController: ControllerBase
{
    [HttpPost("CreateCard")]
    public void CreateCard(IFormFile file) {
        using var stream = System.IO.File.Create(file.FileName);
        file.CopyTo(stream);
    }
}
