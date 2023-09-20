using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class DeckDTO
{
    public Guid Id { get; set; }
    public string DeckCode { get; set; }
    public string DeckName { get; set; }
    public DateTime PostingDate { get; set; }
    public bool Standard { get; set; }
    public UserShortObject Owner { get; set; }
    public string DeckType { get; set; }
    public int NumberOfLikes { get; set; }

    public DeckDTO() { }
    
    public DeckDTO(Guid id, string deckCode, string deckName, DateTime postingDate, bool standard, UserAccount owner, DeckType type, int numberOfLikes) {
        Id = id;
        DeckCode = deckCode;
        DeckName = deckName;
        PostingDate = postingDate;
        Standard = standard;
        Owner = new(owner);
        DeckType = type.ToString();
        NumberOfLikes = numberOfLikes;
    }

    public DeckDTO(Deck deck) :this(deck.Id, deck.DeckCode, deck.DeckName, deck.PostingDate, deck.Standard, deck.OwnerAccount, deck.Type, deck.NumberOfLikes) { }
}