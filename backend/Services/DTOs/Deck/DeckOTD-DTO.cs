using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class DeckOTDDTO(Guid id, Guid deckId, string deckCode, string deckName, DateTime postingDate, bool standard, UserAccount? owner, DeckType type, CardRegions deckRegions,
    int numberOfLikes, ICollection<DeckItem> deckContent, DateTime settingDate, bool setAutomatically, UserAccount? settingUser)
{
    public Guid Id { get; set; } = id;
    public DeckDTO Deck { get; set; } = new DeckDTO(deckId, deckCode, deckName, postingDate, standard, owner, type, deckRegions, numberOfLikes, deckContent);
    public DateTime SettingDate { get; set; } = settingDate;
    public bool SetAutomatically { get; set; } = setAutomatically;
    public UserShortObject? SettingUser { get; set; } = settingUser is null ? null : new(settingUser);

    public DeckOTDDTO(DeckOTD deck) 
        :this(deck.Id, deck.Deck.Id, deck.Deck.DeckCode, deck.Deck.DeckName, deck.Deck.PostingDate, deck.Deck.Standard, deck.Deck.OwnerAccount, 
        deck.Deck.Type, deck.Deck.DeckRegions, deck.Deck.NumberOfLikes, deck.Deck.DeckContent, deck.SettingDate, deck.SetAutomatically, deck.DeckSetter) { }
}