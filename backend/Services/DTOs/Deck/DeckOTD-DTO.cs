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
        :this(deck.Id, deck.Deck.DeckCode, deck.Deck.DeckName, deck.Deck.PostingDate, deck.Deck.Standard, deck.Deck.OwnerAccount, 
        deck.Deck.Type, deck.Deck.NumberOfLikes, deck.SettingDate, deck.SetAutomatically, deck.DeckSetter) { }
}