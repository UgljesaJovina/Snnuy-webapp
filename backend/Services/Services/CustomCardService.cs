using Repositories.Interfaces;
using Repositories.Models;
using Repositories.Utility;
using Services.DTOs;
using Services.Interfaces;
using Repositories.Enums;

namespace Services.Services;

public class CustomCardService : ICustomCardService
{
    private readonly ICustomCardRepo cardRepo;

    public CustomCardService(ICustomCardRepo _cardRepo) {
        cardRepo = _cardRepo;
    }

    public async Task<ICollection<CustomCardDTO>> GetAllCards()
    {
        return (await cardRepo.GetAll()).Select(x => new CustomCardDTO(x)).ToList();
    }

    public async Task<ICollection<CustomCardDTO>> GetAllCardsFromUser(Guid id)
    {
        return (await cardRepo.GetAll(x => x.OwnerAccount.Id == id)).Select(x => new CustomCardDTO(x)).ToList();
    }

    public async Task<ICollection<CustomCardDTO>> GetAllNonValidated() 
    {
        return (await cardRepo.GetAll(x => x.State != CustomCardApprovalState.Approved)).Select(x => new CustomCardDTO(x)).ToList();
    }

    public async Task<CustomCardOTDDTO> GetLatestCardOfTheDay()
    {
        return new(await cardRepo.GetLastCustomCardOTD());
    }

    public async Task<ICollection<CustomCardOTDDTO>> GetAllCardsOfTheDay()
    {
        return (await cardRepo.GetAllCustomCardsOTD()).Select(x => new CustomCardOTDDTO(x)).ToList();
    }

    public async Task<CustomCardDTO> CreateCard(CustomCardCreationRequset requset)
    {
        if (requset.CardDescription.Length > 500 || requset.CardName.Length > 50) throw new ArgumentException("Card description or card name is too long!");

       return new CustomCardDTO(await cardRepo.Create(requset.GetCustomCard()));
    }

    public async Task LikeACard(Guid id, UserAccount account)
    {
        await cardRepo.LikeACard(id, account);
    }

    public async Task<CustomCardDTO> ValidateCustomCard(Guid cardId, UserAccount account, bool approvalState) {
        return new(await cardRepo.ValidateCustomCard(cardId, account, approvalState));
    }
}