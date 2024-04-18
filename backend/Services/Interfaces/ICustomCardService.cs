using Repositories.Filters;
using Repositories.Models;
using Services.DTOs;

namespace Services.Interfaces;

public interface ICustomCardService
{
    public Task<ICollection<CustomCardDTO>> GetAllCards();
    public Task<ICollection<CustomCardDTO>> GetAllCards(Guid id);
    public Task<ICollection<CustomCardDTO>> GetAllCards(CardFilter filter);
    public Task<ICollection<CustomCardDTO>> GetAllNonValidated();
    public Task<CustomCardOTDDTO> GetLatestCardOfTheDay();
    public Task<ICollection<CustomCardOTDDTO>> GetAllCardsOfTheDay();
    public Task<CardLikeRecord> LikeACard(Guid id, UserAccount account);
    public Task<CustomCardDTO> CreateCard(CustomCardCreationRequset requset);
    public Task<CustomCardDTO> ValidateCustomCard(Guid cardId, UserAccount account, bool approvalState);
}