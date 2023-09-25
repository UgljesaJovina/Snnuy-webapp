using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repositories.Enums;
using LorDeckEncoder = Kunc.RiotGames.Lor.DeckCodes.LorDeckEncoder;
using KuncDeckItem = Kunc.RiotGames.Lor.DeckCodes.DeckItem;
using Repositories.Utility;

namespace Repositories.Models;

public class Deck
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string DeckCode { get; set; }
    [MaxLength(50)]
    public string DeckName { get; set; }
    public DateTime PostingDate { get; set; } = DateTime.Now;
    public UserAccount? OwnerAccount { get; set; }
    ICollection<DeckItem> deckContent;
    
    [NotMapped]
    public bool Standard { get { return !DeckContent.Select(x => x.Card).Any(x => !x.Standard); } }
    [NotMapped]
    public ICollection<DeckItem> DeckContent { 
        get {
            if (deckContent is null) {
                var deckItems = new LorDeckEncoder().GetDeckFromCode<KuncDeckItem>(DeckCode);
                deckContent = deckItems.Select(x => new DeckItem() { Count = x.Count, Card = Utils.Cards.First(c => c.CardCode == x.CardCode) }).ToList();
            }

            return deckContent;
        } 
    }
    [NotMapped] // nalazim sve karte koje imaju samo jedan region, i onda postavljam njihove regione kao regione deka, malo glupo al jbg
    public CardRegions DeckRegions { get {
        return (CardRegions)DeckContent.Select(x => (int)x.Card.Regions).Where(x => (x & (x - 1)) == 0).Distinct().Sum();
    } }
    public DeckType Type { get; set; }
    public int NumberOfLikes { get; set; } = 0;
    public ICollection<UserAccount> LikedUsers { get; set; } = new List<UserAccount>();

    public Deck() { }

    public Deck(string deckCode, string deckName, UserAccount owner, DeckType type) {
        DeckCode = deckCode;
        DeckName = deckName;
        OwnerAccount = owner;
        Type = type;
    }

    public bool CheckIfValidCode() {
        try {
            new LorDeckEncoder().GetDeckFromCode<KuncDeckItem>(DeckCode);
            return true;
        } catch (Exception) {
            return false;
        }
    }
}