using Microsoft.AspNetCore.Mvc;
using Repositories.Models;
using Services.Interfaces;
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
    }
}