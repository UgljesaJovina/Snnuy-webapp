using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class DeckOTDDTO : DeckDTO
{
    public DateTime SettingDate { get; set; } = DateTime.Now;
    public bool SetAutomatically { get; set; }
    public UserShortObject? SettingUser { get; set; }

    public DeckOTDDTO() { }

    public DeckOTDDTO(Guid id, string deckCode, string deckName, DateTime postingDate, bool standard, UserAccount owner, DeckType type, int numberOfLikes, DateTime settingDate, bool setAutomatically, UserAccount? settingUser)
    :base(id, deckCode, deckName, postingDate, standard, owner, type, numberOfLikes) {
        SettingDate = settingDate;
        SetAutomatically = setAutomatically;
        SettingUser = settingUser is null ? null : new(settingUser);
    }

    public DeckOTDDTO(DeckOTD deck) 
        :this(deck.Id, deck.DeckCode, deck.DeckName, deck.PostingDate, deck.Standard, deck.OwnerAccount, 
        deck.Type, deck.LikedUsers.Count, deck.SettingDate, deck.SetAutomatically, deck.DeckSetter) { }
}