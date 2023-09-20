using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Repositories.Models;
using WebApi.Utils.Attributes;
using Services.Interfaces;
using Services.DTOs;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomCardController: ControllerBase
{
    private readonly ICustomCardService cardService;

    public CustomCardController(ICustomCardService service) {
        cardService = service;
    }

    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ICollection<CustomCardDTO>> GetAllCards() {
        return await cardService.GetAllCards();
    }

    [HttpGet("GetAllFromUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ICollection<CustomCardDTO>> GetAllCardsFromUser(Guid id) {
        return await cardService.GetAllCardsFromUser(id);
    }

    [HttpGet("GetLatestCardOTD")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<CustomCardOTDDTO> GetLatestCardOTD() {
        return await cardService.GetLatestCardOfTheDay();
    }

    [HttpGet("GetAllCardsOTD")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ICollection<CustomCardOTDDTO>> GetAllCardsOTD() {
        return await cardService.GetAllCardsOfTheDay();
    }

    [HttpPatch("LikeACard")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [Authorize(Repositories.Enums.UserPermissions.RateCustomCard)]
    public async Task<IActionResult> LikeACard(Guid cardId) {
        try {
            await cardService.LikeACard(cardId, (UserAccount)HttpContext.Items["User"]!);
            return Ok();
        } catch(KeyNotFoundException ex) {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("CreateACard")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [Authorize(Repositories.Enums.UserPermissions.SubmitCustomCard)]
    public async Task<ActionResult<CustomCardDTO>> CreateACard([FromForm]CustomCardExtendedRequest request) {
        try {
            request.DataStream = request.ImageFile.OpenReadStream();
            request.Owner = new();
            // (UserAccount)HttpContext.Items["User"]!;
            return Ok(await cardService.CreateCard(request));
        } catch (ArgumentException ex) {
            return BadRequest(ex.Message);
        }
    }
}

public class CustomCardExtendedRequest : CustomCardCreationRequset
{
    public IFormFile ImageFile { get; set; }
}
