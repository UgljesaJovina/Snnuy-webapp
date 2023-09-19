using Repositories.Models;
using Services.DTOs;

namespace Services.Interfaces;

public interface IDeckRepo
{
    public Task<ICollection<DeckShortObject>> GetAllDecksOTD();
    public Task<DeckShortObject> GetLastDeckOTD();
    public Task LikeADeck(Guid id, UserAccount account);
    public Task<DeckDetailObject> GetDeckInfo(Guid id);
}