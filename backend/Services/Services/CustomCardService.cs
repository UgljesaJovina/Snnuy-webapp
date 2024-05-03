using Repositories.Interfaces;
using Repositories.Models;
using Repositories.Utility;
using Services.DTOs;
using Services.Interfaces;
using Repositories.Enums;
using Repositories.Filters;

namespace Services.Services;

public class CustomCardService(ICustomCardRepo _cardRepo) : ICustomCardService
{
    private readonly ICustomCardRepo cardRepo = _cardRepo;
    readonly string cardPath = Path.Combine("public", "CustomCards");

    public async Task<ICollection<CustomCardDTO>> GetAllCards()
    {
        return (await cardRepo.GetAll()).Select(x => new CustomCardDTO(x)).ToList();
    }

    public async Task<ICollection<CustomCardDTO>> GetAllCards(Guid id)
    {
        return (await cardRepo.GetAll(x => x.OwnerAccount?.Id == id)).Select(x => new CustomCardDTO(x)).ToList();
    }

    public async Task<ICollection<CustomCardDTO>> GetAllCards(CardFilter filter) {
        return (await cardRepo.GetAll(filter)).Select(x => new CustomCardDTO(x)).ToList();
    }

    public async Task<ICollection<CustomCardDTO>> GetAllNonValidated() 
    {
        return (await cardRepo.GetAll(x => x.ApprovalState != CustomCardApprovalState.Approved)).Select(x => new CustomCardDTO(x)).ToList();
    }

    public async Task<CustomCardOTDDTO> GetLatestCardOfTheDay()
    {
        return new(await cardRepo.GetLatestCustomCardOTD());
    }

    public async Task<ICollection<CustomCardOTDDTO>> GetAllCardsOfTheDay()
    {
        return (await cardRepo.GetAllCustomCardsOTD()).Select(x => new CustomCardOTDDTO(x)).ToList();
    }

    public async Task<CustomCardDTO> CreateCard(CustomCardCreationRequset requset)
    {
        if (requset.CardDescription.Length > 500 || requset.CardName.Length > 50) throw new ArgumentException("Card description or card name is too long!");

        using var stream = File.Create(Path.Combine(cardPath, requset.CardImageName));
        await requset.DataStream.CopyToAsync(stream);

       return new CustomCardDTO(await cardRepo.Create(requset.GetCustomCard()));
    }

    public async Task<CardLikeRecord> LikeACard(Guid id, UserAccount account)
    {
        return await cardRepo.LikeACard(id, account);
    }

    public async Task<CustomCardDTO> ValidateCustomCard(Guid cardId, UserAccount account, bool approvalState) {
        return new(await cardRepo.ValidateCustomCard(cardId, account, approvalState));
    }

    public async Task<CustomCardOTDDTO> SetCustomCardOTD(Guid id, UserAccount acc)
    {
        return new(await cardRepo.SetCustomCardOTD(id, acc));
    }
}