using Repositories.Models;

namespace Repositories.Interfaces;

public interface IDeckRepo : IRepository<Deck>
{
    public Task<DeckOTD> SetDeckOfTheDay(Guid deckId, UserAccount? account = null);
    public Task<ICollection<DeckOTD>> GetAllDecksOTD();
    public Task<DeckOTD> GetLastDeckOTD();
    public Task LikeADeck(Guid id, UserAccount account);
}