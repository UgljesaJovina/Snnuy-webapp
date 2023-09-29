using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Repositories.Models;
using WebApi.Utils.Attributes;
using Services.Interfaces;
using Services.DTOs;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Repositories.Enums;

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

    [HttpGet("GetAllFromUser/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ICollection<CustomCardDTO>> GetAllCardsFromUser(Guid userId) {
        return await cardService.GetAllCardsFromUser(userId);
    }

    [HttpGet("GetAllNonValid")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(UserPermissions.ValidateCustomCard)]
    public async Task<ICollection<CustomCardDTO>> GetAllNonValidatedCards() {
        return await cardService.GetAllNonValidated();
    }

    [HttpGet("GetLatestCardOTD")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CustomCardOTDDTO>> GetLatestCardOTD() {
        try {
            return Ok(await cardService.GetLatestCardOfTheDay());
        } catch(KeyNotFoundException ex) { 
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("GetAllCardsOTD")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ICollection<CustomCardOTDDTO>> GetAllCardsOTD() {
        return await cardService.GetAllCardsOfTheDay();
    }

    [HttpPatch("LikeACard/{cardId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
    [Authorize(UserPermissions.RateCustomCard)]
    public async Task<IActionResult> LikeACard(Guid cardId) {
        try {
            await cardService.LikeACard(cardId, (UserAccount)HttpContext.Items["User"]!);
            return Ok();
        } catch(KeyNotFoundException ex) {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("CreateACard")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
    [Authorize(UserPermissions.SubmitCustomCard)]
    public async Task<ActionResult<CustomCardDTO>> CreateACard([FromForm]CustomCardExtendedRequest request) {
        try {
            request.DataStream = request.ImageFile.OpenReadStream();
            request.Owner = (UserAccount)HttpContext.Items["User"]!;
            return Ok(await cardService.CreateCard(request));
        } catch (ArgumentException ex) {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("ValidateACard/{cardId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
    [Authorize(UserPermissions.ValidateCustomCard)]
    public async Task<ActionResult<CustomCardDTO>> ValidateACard(Guid cardId, [FromQuery]bool approvalState) {
        try {
            return Ok(await cardService.ValidateCustomCard(cardId, (UserAccount)HttpContext.Items["User"]!, approvalState));
        } catch(KeyNotFoundException ex) {
            return NotFound(new { message = ex.Message });
        }
    }
}

public class CustomCardExtendedRequest : CustomCardCreationRequset
{
    public IFormFile ImageFile { get; set; }
}
