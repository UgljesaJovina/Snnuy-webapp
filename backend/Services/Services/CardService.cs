using Repositories.Interfaces;
using Repositories.Models;
using Services.DTOs;

namespace Services.Services;

public class CardService : ICardService
{
    readonly ICustomCardRepo cardRepo;

    public CardService(ICustomCardRepo _cardRepo) {
        cardRepo = _cardRepo;
    }

    public async Task<ICollection<CustomCardDTO>> GetAll()
    {
        return (await cardRepo.GetAll()).Select(x => new CustomCardDTO(x)).ToList();
    }

    public async Task<ICollection<CustomCardDTO>> GetAllCardsFromUser(Guid id)
    {
        return (await cardRepo.GetAll(x => x.OwnerAccount.Id == id)).Select(x => new CustomCardDTO(x)).ToList();
    }

    public async Task<CustomCardOTDDTO> GetLatestCardOfTheDay()
    {
        return new(await cardRepo.GetLastCustomCardOTD());
    }

    public async Task<ICollection<CustomCardOTDDTO>> GetAllCardsOfTheDay()
    {
        return (await cardRepo.GetAllCustomCardsOTD()).Select(x => new CustomCardOTDDTO(x)).ToList();
    }

    public async Task<CustomCardDTO> CreateCard(CustomCardCreationRequset requset, Stream stream)
    {
        if (requset.CardDescription.Length > 500 || requset.CardName.Length > 50) throw new ArgumentException("Card description or card name is too long!");
        
    }
}