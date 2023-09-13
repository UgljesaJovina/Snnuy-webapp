using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class DeckShortObject
{
    public Guid Id { get; set; }
    public string DeckCode { get; set; }
    public string DeckName { get; set; }
    public DateTime PostingDate { get; set; }
    public bool Standard { get; set; }
    public string OwnerUsername { get; set; }
    public string DeckType { get; set; }
    public int NumberOfLikes { get; set; }

    public DeckShortObject() { }
    
    public DeckShortObject(Guid id, string deckCode, string deckName, DateTime postingDate, bool standard, UserAccount owner, DeckType type, int numberOfLikes) {
        Id = id;
        DeckCode = deckCode;
        DeckName = deckName;
        PostingDate = postingDate;
        Standard = standard;
        OwnerUsername = owner.Username;
        DeckType = type.ToString();
        NumberOfLikes = numberOfLikes;
    }

    public DeckShortObject(Deck deck) :this(deck.Id, deck.DeckCode, deck.DeckName, deck.PostingDate, deck.Standard, deck.OwnerAccount, deck.Type, deck.LikedUsers.Count()) { }
}