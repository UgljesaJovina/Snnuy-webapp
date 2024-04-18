using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class CustomCardDTO
{
    public Guid Id { get; set; }
    public string CardName { get; set; }
    public DateTime PostingDate { get; set; }
    public UserShortObject? Owner { get; set; }
    public CardRegions Regions { get; set; }
    public CardTypes CardType { get; set; }
    public CustomCardApprovalState CardApprovalState { get; set; }
    public int NumberOfLikes { get; set; }

    public CustomCardDTO() { }

    public CustomCardDTO(Guid id, string cardName, DateTime postingDate, UserAccount? owner, CardRegions regions, CardTypes type, CustomCardApprovalState state, int numberOfLikes) {
        Id = id;
        CardName = cardName;
        PostingDate = postingDate;
        Owner = owner is null ? null : new(owner);
        Regions = regions;
        CardType = type;
        CardApprovalState = state;
        NumberOfLikes = numberOfLikes;
    }

    public CustomCardDTO(CustomCard card) :this(card.Id, card.CardName, card.PostingDate, card.OwnerAccount, card.Regions, card.Type, card.ApprovalState, card.NumberOfLikes) { }
}