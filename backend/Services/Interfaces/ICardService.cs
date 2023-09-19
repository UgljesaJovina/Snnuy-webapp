namespace Services.DTOs;

public interface ICardService
{
    public Task<ICollection<CustomCardDTO>> GetAll();
    public Task<ICollection<CustomCardDTO>> GetAllCardsFromUser(Guid id);
    public Task<CustomCardOTDDTO> GetLatestCardOfTheDay();
    public Task<ICollection<CustomCardOTDDTO>> GetAllCardsOfTheDay();
    public Task<CustomCardDTO> CreateCard(CustomCardCreationRequset requset);
}