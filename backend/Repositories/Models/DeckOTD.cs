namespace Repositories.Models;

public class DeckOTD
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Deck Deck { get; set; }
    public DateTime SettingDate { get; set; }
    public bool SetAutomatically { get; set; }
    public UserAccount? DeckSetter { get; set; }

    public DeckOTD() { }

    public DeckOTD(Deck deck, bool setAutomatically, UserAccount? deckSetter = null) {
        Deck = deck;
        SetAutomatically = setAutomatically;
        DeckSetter = deckSetter;
    }
}