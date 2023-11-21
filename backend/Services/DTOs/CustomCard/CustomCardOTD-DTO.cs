using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class CustomCardOTDDTO
{
    public Guid Id { get; set; }
    public CustomCardDTO Card { get; set; }
    public DateTime SettingDate { get; set; }
    public bool SetAutomatically { get; set; }
    public UserShortObject? SettingUser { get; set; }

    public CustomCardOTDDTO() { }

    public CustomCardOTDDTO(Guid id, Guid cardId, string cardName, DateTime postingDate, UserAccount? owner, CardRegions regions,
        CardTypes type, DateTime settingDate, bool setAutomatically, UserAccount? cardSetter, int numberOfLikes)
    {
        Id = id;
        Card = new(cardId, cardName, postingDate, owner, regions, type, CustomCardApprovalState.Approved, numberOfLikes);
        SettingDate = settingDate;
        SetAutomatically = setAutomatically;
        SettingUser = cardSetter is null ? null : new(cardSetter);
    }

    public CustomCardOTDDTO(CustomCardOTD card) :this(card.Id, card.Card.Id, card.Card.CardName, card.Card.PostingDate, card.Card.OwnerAccount,
        card.Card.Regions, card.Card.Type, card.SettingDate, card.SetAutomatically, card.CardSetter, card.Card.NumberOfLikes) { }
}