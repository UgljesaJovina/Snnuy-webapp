using Repositories.Enums;

namespace Repositories.Models;

public class DeckOTD : Deck
{
    public DateTime SettingDate { get; set; }
    public bool SetAutomatically { get { return DeckSetter is null; } }
    public UserAccount? DeckSetter { get; set; }

    public DeckOTD() { }

    public DeckOTD(string deckCode, string deckName, UserAccount owner, DeckType type, UserAccount? deckSetter = null)
    :base(deckCode, deckName, owner, type) {
        DeckSetter = deckSetter;
    }

    public DeckOTD(Deck deck, UserAccount? deckSetter = null) 
    :this(deck.DeckCode, deck.DeckName, deck.OwnerAccount, deck.Type, deckSetter) { }
}