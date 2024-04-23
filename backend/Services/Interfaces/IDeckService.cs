using Repositories.Filters;
using Repositories.Models;
using Services.DTOs;

namespace Services.Interfaces;

public interface IDeckService
{
    public Task<ICollection<DeckDTO>> GetAll();
    public Task<ICollection<DeckDTO>> GetAll(DeckFilter filter);
    public Task<ICollection<DeckDTO>> GetAll(Guid userId);
    public Task<DeckOTDDTO> GetLatestDeckOTD();
    public Task<ICollection<DeckOTDDTO>> GetAllDecksOTD();
    public Task LikeADeck(Guid id, UserAccount account);
    public Task<DeckDTO> CreateDeck(DeckCreationRequest request);
    public Task<DeckDetailedDTO> GetDeckInfo(Guid id);
}