using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class CustomCardDTO(Guid id, string cardName, string cardDescription, string imagePath, DateTime postingDate,
    UserAccount? owner, CardRegions regions, CardTypes type, CustomCardApprovalState state, int numberOfLikes)
{
    public Guid Id { get; set; } = id;
    public string CardName { get; set; } = cardName;
    public string CardDescription { get; set; } = cardDescription;
    public string CardImagePath { get; set; } = imagePath;
    public DateTime PostingDate { get; set; } = postingDate;
    public UserShortObject? Owner { get; set; } = owner is null ? null : new(owner);
    public CardRegions Regions { get; set; } = regions;
    public CardTypes CardType { get; set; } = type;
    public CustomCardApprovalState CardApprovalState { get; set; } = state;
    public int NumberOfLikes { get; set; } = numberOfLikes;

    public CustomCardDTO(CustomCard card): this(card.Id, card.CardName, card.CardDescription, card.CardImageName, 
        card.PostingDate, card.OwnerAccount, card.Regions, card.Type, card.ApprovalState, card.NumberOfLikes) { }
}