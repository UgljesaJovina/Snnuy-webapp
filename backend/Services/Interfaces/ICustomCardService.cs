using Repositories.Models;
using Services.DTOs;

namespace Services.Interfaces;

public interface ICustomCardService
{
    public Task<ICollection<CustomCardDTO>> GetAllCards();
    public Task<ICollection<CustomCardDTO>> GetAllCards(Guid id);
    public Task<ICollection<CustomCardDTO>> GetAllNonValidated();
    public Task<CustomCardOTDDTO> GetLatestCardOfTheDay();
    public Task<ICollection<CustomCardOTDDTO>> GetAllCardsOfTheDay();
    public Task LikeACard(Guid id, UserAccount account);
    public Task<CustomCardDTO> CreateCard(CustomCardCreationRequset requset);
    public Task<CustomCardDTO> ValidateCustomCard(Guid cardId, UserAccount account, bool approvalState);
}