using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repositories.Enums;
using LorDeckEncoder = Kunc.RiotGames.Lor.DeckCodes.LorDeckEncoder;
using KuncDeckItem = Kunc.RiotGames.Lor.DeckCodes.DeckItem;
using Repositories.Utility;

namespace Repositories.Models;

public class Deck
{
    public Guid Id { get; set; }
    public string DeckCode { get; set; }
    [MaxLength(50)]
    public string DeckName { get; set; }
    public DateTime PostingDate { get; set; }
    public bool Eternal { get; set; }
    public UserAccount OwnerAccount { get; set; }
    public virtual ICollection<UserAccount> LikedUsers { get; set; } = new List<UserAccount>();
    [NotMapped]
    public ICollection<DeckItem> DeckContent { get; set; }
    public DeckType Type { get; set; }

    public Deck() {
        var deckItems = new LorDeckEncoder().GetDeckFromCode<KuncDeckItem>(DeckCode);
        DeckContent = deckItems.Select(x => new DeckItem() { Count = x.Count, Card = Utils.Cards.First(c => c.CardCode == x.CardCode) }).ToList();
    }
}