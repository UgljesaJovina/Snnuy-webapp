using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class CustomCardOTDDTO(Guid id, Guid cardId, string cardName, string cardDescription, string imagePath,
    DateTime postingDate, UserAccount? owner, CardRegions regions, CardTypes type, DateTime settingDate,
    bool setAutomatically, UserAccount? cardSetter, int numberOfLikes)
{
    public Guid Id { get; set; } = id;
    public CustomCardDTO Card { get; set; } = new(cardId, cardName, cardDescription, imagePath, postingDate, owner,
        regions, type, CustomCardApprovalState.Approved, numberOfLikes);
    public DateTime SettingDate { get; set; } = settingDate;
    public bool SetAutomatically { get; set; } = setAutomatically;
    public UserShortObject? SettingUser { get; set; } = cardSetter is null ? null : new(cardSetter);

    public CustomCardOTDDTO(CustomCardOTD card) :this(card.Id, card.Card.Id, card.Card.CardName,
        card.Card.CardDescription, card.Card.CardImageName, card.Card.PostingDate, card.Card.OwnerAccount,
        card.Card.Regions, card.Card.Type, card.SettingDate, card.SetAutomatically, card.CardSetter,
        card.Card.NumberOfLikes) { }
}