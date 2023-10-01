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
    public UserShortObject? Owner { get; set; }
    public string DeckType { get; set; }
    public string DeckRegions { get; set; }
    public int NumberOfLikes { get; set; }
    public ICollection<string> Champions { get; set; }

    public DeckDTO() { }
    
    public DeckDTO(Guid id, string deckCode, string deckName, DateTime postingDate, bool standard, UserAccount? owner, DeckType type, CardRegions deckRegions, 
        int numberOfLikes, ICollection<DeckItem> deckContent) 
    {
        Id = id;
        DeckCode = deckCode;
        DeckName = deckName;
        PostingDate = postingDate;
        Standard = standard;
        Owner = owner is null ? null : new(owner);
        DeckType = type.ToString();
        DeckRegions = deckRegions.ToString();
        NumberOfLikes = numberOfLikes;
        Champions = deckContent.Where(x => x.Card.Rarity == CardRarity.Champion).Select(x => x.Card.CardName).ToList();
    }

    public DeckDTO(Deck deck) :this(deck.Id, deck.DeckCode, deck.DeckName, deck.PostingDate, deck.Standard, deck.OwnerAccount, deck.Type, deck.DeckRegions, deck.NumberOfLikes, deck.DeckContent)
        { }
}