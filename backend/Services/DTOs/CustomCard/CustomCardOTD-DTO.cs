using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class CustomCardOTDDTO : CustomCardDTO
{
    public DateTime SettingDate { get; set; }
    public bool SetAutomatically { get; set; }
    public UserShortObject? SettingUser { get; set; }

    public CustomCardOTDDTO() { }

    public CustomCardOTDDTO(Guid id, string cardName, DateTime postingDate, UserAccount owner,
        CardTypes type, DateTime settingDate, bool setAutomatically, UserAccount? cardSetter, int numberOfLikes)
        :base(id, cardName, postingDate, owner, type, CustomCardApprovalState.Approved, numberOfLikes)
    {
        SettingDate = settingDate;
        SetAutomatically = setAutomatically;
        SettingUser = cardSetter is null ? null : new(cardSetter);
    }

    public CustomCardOTDDTO(CustomCardOTD card) :this(card.Id, card.CardName, card.PostingDate, card.OwnerAccount,
        card.Type, card.SettingDate, card.SetAutomatically, card.CardSetter, card.NumberOfLikes) { }
}