using Repositories.Models;

namespace Repositories.Interfaces;

public interface IDeckRepo : IRepository<Deck>
{
    public Task<DeckOTD> SetDeckOfTheDay(Guid deckId, UserAccount? account = null);
}