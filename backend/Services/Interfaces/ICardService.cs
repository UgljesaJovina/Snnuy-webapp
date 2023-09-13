namespace Services.DTOs;

public interface ICardService
{
    public Task<ICollection<CustomCardDTO>> GetAll();
    public Task<ICollection<CustomCardDTO>> GetCardsAllFromUser(Guid id);
    public Task<
}