using Repositories.Filters;
using Repositories.Models;

namespace Repositories.Interfaces;

public interface IDeckRepo : IRepository<Deck>
{
    public Task<ICollection<Deck>> GetAll(DeckFilter filter);
    public Task<DeckOTD> SetDeckOfTheDay(Guid deckId, UserAccount? account = null, Deck? deck = null);
    public Task<ICollection<DeckOTD>> GetAllDecksOTD();
    public Task<DeckOTD> GetLatestDeckOTD();
    public Task<DeckLikeRecord> LikeADeck(Guid id, UserAccount account);
}