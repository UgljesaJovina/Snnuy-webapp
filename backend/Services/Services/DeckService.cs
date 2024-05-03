using Repositories.Filters;
using Repositories.Interfaces;
using Repositories.Models;
using Services.DTOs;
using Services.Interfaces;

namespace Services.Services;

public class DeckService(IDeckRepo _deckRepo) : IDeckService
{
    private readonly IDeckRepo deckRepo = _deckRepo;

    public async Task<ICollection<DeckDTO>> GetAll()
    {
        return (await deckRepo.GetAll()).Select(x => new DeckDTO(x)).ToList();
    }

    public async Task<ICollection<DeckDTO>> GetAll(DeckFilter filter)
    {
        return (await deckRepo.GetAll(filter)).Select(x => new DeckDTO(x)).ToList();
    }

    public async Task<ICollection<DeckOTDDTO>> GetAllDecksOTD()
    {
        return (await deckRepo.GetAllDecksOTD()).Select(x => new DeckOTDDTO(x)).ToList();
    }

    public async Task<DeckDTO> CreateDeck(DeckCreationRequest request)
    {
        if (request.DeckName.Length > 50) throw new ArgumentException("The name of the deck is too long.");

        Deck deck = request.GetDeck();

        if (!deck.CheckIfValidCode()) throw new ArgumentException("The code of the deck is not valid!");

        return new(await deckRepo.Create(deck));
    }

    public async Task<ICollection<DeckDTO>> GetAll(Guid userId)
    {
        return (await deckRepo.GetAll(x => x.Id == userId)).Select(x => new DeckDTO(x)).ToList();
    }

    public async Task<DeckDetailedDTO> GetDeckInfo(Guid id)
    {
        return new(await deckRepo.GetById(id));
    }

    public async Task<DeckOTDDTO> GetLatestDeckOTD()
    {
        return new(await deckRepo.GetLatestDeckOTD());
    }

    public async Task LikeADeck(Guid id, UserAccount account)
    {
        await deckRepo.LikeADeck(id, account);
    }
}