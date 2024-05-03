using Microsoft.AspNetCore.Mvc;
using Repositories.Models;
using WebApi.Utils.Attributes;
using Services.Interfaces;
using Services.DTOs;
using Repositories.Enums;
using System.Drawing;
using Repositories.Filters;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomCardController: ControllerBase
{
    private readonly ICustomCardService cardService;

    public CustomCardController(ICustomCardService service) {
        cardService = service;
    }

    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ICollection<CustomCardDTO>> GetAllCards() {
        return await cardService.GetAllCards();
    }

    [HttpGet("get-all/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ICollection<CustomCardDTO>> GetAllCardsFromUser(Guid userId) {
        return await cardService.GetAllCards(userId);
    }

    [HttpGet("get-all-filtered")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ICollection<CustomCardDTO>> GetAllCardsFiltered([FromQuery]CardFilter filter)
    {
        return await cardService.GetAllCards(filter);
    }

    [HttpGet("get-all-non-valid")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(UserPermissions.ValidateCustomCard)]
    public async Task<ICollection<CustomCardDTO>> GetAllNonValidatedCards() {
        return await cardService.GetAllNonValidated();
    }

    [HttpGet("get-latest-card-otd")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CustomCardOTDDTO>> GetLatestCardOTD() {
        try {
            return Ok(await cardService.GetLatestCardOfTheDay());
        } catch(KeyNotFoundException ex) { 
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("set-custom-card-otd/{cardId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(UserPermissions.SetCustomCardOfTheDay)]
    public async Task<ActionResult<CustomCardOTD>> SetCustomCardOTD(Guid cardId) {
        try {
            return Ok(await cardService.SetCustomCardOTD(cardId, (UserAccount)HttpContext.Items["User"]!));
        } catch(KeyNotFoundException ex) {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("get-all-cards-otd")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ICollection<CustomCardOTDDTO>> GetAllCardsOTD() {
        return await cardService.GetAllCardsOfTheDay();
    }

    [HttpPatch("like-a-card/{cardId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
    [Authorize(UserPermissions.RateCustomCard)]
    public async Task<ActionResult<CardLikeRecord>> LikeACard(Guid cardId) {
        try {
            return Ok(await cardService.LikeACard(cardId, (UserAccount)HttpContext.Items["User"]!));
        } catch(KeyNotFoundException ex) {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("create-a-card")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
    [Authorize(UserPermissions.SubmitCustomCard)]
    public async Task<ActionResult<CustomCardDTO>> CreateACard([FromForm]CustomCardExtendedRequest request) {
        try {
            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(request.ImageFile.FileName);
            request.CardImageName = imageName;
            request.DataStream = request.ImageFile.OpenReadStream();
            request.Owner = (UserAccount)HttpContext.Items["User"]!;
            return Ok(await cardService.CreateCard(request));
        } catch (ArgumentException ex) {
            return BadRequest(new { message = ex.Message });
        } catch (Exception ex) {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("validate-a-card/{cardId}")]
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
