using Repositories.Models;
using Services.DTOs;

namespace Services.Interfaces;

public interface IDeckService
{
    public Task<ICollection<DeckDTO>> GetAll();
    public Task<ICollection<DeckDTO>> GetAllFromUser(Guid userId);
    public Task<DeckOTDDTO> GetLatestDeckOTD();
    public Task<ICollection<DeckOTDDTO>> GetAllDecksOTD();
    public Task LikeADeck(Guid id, UserAccount account);
    public Task<DeckDTO> CreateDeck(DeckCreationRequest request);
    public Task<DeckDetailedDTO> GetDeckInfo(Guid id);
}