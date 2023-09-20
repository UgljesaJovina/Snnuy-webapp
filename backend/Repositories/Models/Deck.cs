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
    public UserAccount OwnerAccount { get; set; }
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
    public DeckType Type { get; set; }
    public int NumberOfLikes { get; set; } = 0;
    public ICollection<UserAccount> LikedUsers { get; set; } = new List<UserAccount>();

    public Deck() { }

    public Deck(string deckCode, string deckName, UserAccount owner, DeckType type) : this() {
        DeckCode = deckCode;
        DeckName = deckName;
        OwnerAccount = owner;
        Type = type;
    }

    public bool CheckIfValidCode() {
        try {
            new LorDeckEncoder().GetDeckFromCode<KuncDeckItem>(DeckCode);
        } catch (ArgumentException) {
            return false;
        }

        return true;
    }
}