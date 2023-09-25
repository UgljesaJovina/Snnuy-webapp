using Microsoft.AspNetCore.Mvc;
using Repositories.Enums;
using Repositories.Models;
using Services.DTOs;
using Services.Interfaces;
using WebApi.Utils.Attributes;
namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeckController : ControllerBase
    {
        private readonly IDeckService deckService;

        public DeckController(IDeckService service) {
            deckService = service;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ICollection<DeckDTO>> GetAll() {
            return await deckService.GetAll();
        }

        [HttpGet("GetAllFromUser/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ICollection<DeckDTO>> GetAll(Guid id) {
            return await deckService.GetAllFromUser(id);
        }

        
        [HttpGet("GetLatestDeckOTD")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<DeckOTDDTO> GetLatestDeckOTD() {
            return await deckService.GetLatestDeckOTD();
        }

        
        [HttpGet("GetAllDecksOTD")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ICollection<DeckOTDDTO>> GetAllDecksOTD() {
            return await deckService.GetAllDecksOTD();
        }
        
        [HttpPut("LikeADeck/{deckId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        [Authorize(UserPermissions.RateDeck)]
        public async Task<ActionResult> LikeADeck(Guid deckId) {
            try {
                await deckService.LikeADeck(deckId, (UserAccount)HttpContext.Items["User"]!);
                return Ok();
            } catch (KeyNotFoundException ex) {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("CreateADeck")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [Authorize(UserPermissions.SubmitDeck)]
        public async Task<ActionResult<DeckDTO>> CreateADeck(DeckCreationRequest request) {
            try {
                request.Owner = (UserAccount)HttpContext.Items["User"]!;
                return Ok(await deckService.CreateDeck(request));
            } catch (ArgumentException ex) {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetDeckInfo/{deckId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        public async Task<ActionResult<DeckDetailedDTO>> GetDeckInfo(Guid deckId) {
            try {
                return Ok(await deckService.GetDeckInfo(deckId));
            } catch (KeyNotFoundException ex) {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}