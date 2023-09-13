using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class CardOTDDTO
{

    public Guid Id { get; set; }
    public string CardName { get; set; }
    public DateTime PostingDate { get; set; }
    public string Owner { get; set; }
    public string CardType { get; set; }
    public DateTime SettingDate { get; set; }
    public bool SetAutomatically { get; set; }
    public UserShortObject? SettingUser { get; set; }

    public CardOTDDTO() { }

    public CardOTDDTO(Guid id, string cardName, DateTime postingDate, UserAccount owner,
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

    public CardOTDDTO(CustomCardOTD card) :this(card.Id, card.Card.CardName, card.Card.PostingDate) { }
}