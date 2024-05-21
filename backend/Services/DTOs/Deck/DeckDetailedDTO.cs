using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class DeckDetailedDTO(Guid id, string deckCode, string deckName, DateTime postingDate, bool standard,
    UserAccount owner, int numberOfLikes, ICollection<DeckItem> deckContent, CardRegions deckRegions, DeckType type)
{
    public Guid Id { get; set; } = id;
    public string DeckCode { get; set; } = deckCode;
    public string DeckName { get; set; } = deckName;
    public DateTime PostingDate { get; set; } = postingDate;
    public bool Standard { get; set; } = standard;
    public UserShortObject Owner { get; set; } = new(owner);
    public int NumberOfLikes { get; set; } = numberOfLikes;
    public ICollection<DeckItemDTO> DeckContent { get; set; } = deckContent.Select(x => new DeckItemDTO(x)).ToList();
    public DeckType DeckType { get; set; } = type;
    public CardRegions DeckRegions { get; set; } = deckRegions;
    public int DeckCost { get { return DeckContent.Select(x => x.Rarity).Sum(x => (int)x); }}

    public DeckDetailedDTO(Deck deck) :this(deck.Id, deck.DeckCode, deck.DeckName, deck.PostingDate, deck.Standard,
        deck.OwnerAccount, deck.NumberOfLikes, deck.DeckContent, deck.DeckRegions, deck.Type) { }
}