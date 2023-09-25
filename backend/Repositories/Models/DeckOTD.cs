using System.ComponentModel.DataAnnotations;
using Repositories.Enums;

namespace Repositories.Models;

public class DeckOTD
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public Deck Deck { get; set; }
    public DateTime SettingDate { get; set; }
    public bool SetAutomatically { get { return DeckSetter is null; } }
    public UserAccount? DeckSetter { get; set; }

    public DeckOTD() { }

    public DeckOTD(Deck deck, UserAccount? deckSetter = null)
    {
        Deck = deck;
        DeckSetter = deckSetter;
    }
}