using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class CustomCardOTDDTO
{

    public Guid Id { get; set; }
    public string CardName { get; set; }
    public DateTime PostingDate { get; set; }
    public string Owner { get; set; }
    public string CardType { get; set; }
    public DateTime SettingDate { get; set; }
    public bool SetAutomatically { get; set; }
    public UserShortObject? SettingUser { get; set; }

    public CustomCardOTDDTO() { }

    public CustomCardOTDDTO(Guid id, string cardName, DateTime postingDate, UserAccount owner,
        CardTypes type, DateTime settingDate, bool setAutomatically, UserAccount? cardSetter)
    {
        Id = id;
        CardName = cardName;
        PostingDate = postingDate;
        Owner = owner.Username;
        CardType = type.ToString();
        SettingDate = settingDate;
        SetAutomatically = setAutomatically;
        SettingUser = cardSetter is null ? null : new(cardSetter);
    }

    public CustomCardOTDDTO(CustomCardOTD card) :this(card.Id, card.Card.CardName, card.Card.PostingDate, card.Card.OwnerAccount,
        card.Card.Type, card.SettingDate, card.SetAutomatically, card.CardSetter) { }
}