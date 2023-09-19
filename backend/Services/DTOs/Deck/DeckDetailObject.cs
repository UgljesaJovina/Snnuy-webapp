using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class DeckDetailObject
{
    public Guid Id { get; set; }
    public string DeckCode { get; set; }
    public string DeckName { get; set; }
    public DateTime PostingDate { get; set; }
    public bool Standard { get; set; }
    public UserShortObject Owner { get; set; }
    public int NumberOfLikes { get; set; }
    public ICollection<DeckItem> DeckContent { get; set; }
    public string DeckType { get; set; }

    public DeckDetailObject() { }

    public DeckDetailObject(Guid id, string deckCode, string deckName, DateTime postingDate, bool standard, UserAccount owner, int numberOfLikes, ICollection<DeckItem> deckContent, DeckType type) {
        Id = id;
        DeckCode = deckCode;
        DeckName = deckName;
        PostingDate = postingDate;
        Standard = standard;
        Owner = new(owner);
        NumberOfLikes = numberOfLikes;
        DeckContent = deckContent;
        DeckType = type.ToString();
    }

    public DeckDetailObject(Deck deck) :this(deck.Id, deck.DeckCode, deck.DeckName, deck.PostingDate, deck.Standard, deck.OwnerAccount, deck.LikedUsers.Count, deck.DeckContent, deck.Type) { }
}