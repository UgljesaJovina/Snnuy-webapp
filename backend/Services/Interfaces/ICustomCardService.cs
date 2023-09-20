using Repositories.Models;
using Services.DTOs;

namespace Services.Interfaces;

public interface ICustomCardService
{
    public Task<ICollection<CustomCardDTO>> GetAll();
    public Task<ICollection<CustomCardDTO>> GetAllCardsFromUser(Guid id);
    public Task<CustomCardOTDDTO> GetLatestCardOfTheDay();
    public Task<ICollection<CustomCardOTDDTO>> GetAllCardsOfTheDay();
    public Task LikeACard(Guid id, UserAccount account);
    public Task<CustomCardDTO> CreateCard(CustomCardCreationRequset requset);
}