using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class DeckDetailedDTO
{
    public Guid Id { get; set; }
    public string DeckCode { get; set; }
    public string DeckName { get; set; }
    public DateTime PostingDate { get; set; }
    public bool Standard { get; set; }
    public UserShortObject Owner { get; set; }
    public int NumberOfLikes { get; set; }
    public ICollection<DeckItemDTO> DeckContent { get; set; }
    public string DeckType { get; set; }
    public string DeckRegions { get; set; }
    public int DeckCost { get { return DeckContent.Select(x => x.Rarity).Sum(); }}

    public DeckDetailedDTO() { }

    public DeckDetailedDTO(Guid id, string deckCode, string deckName, DateTime postingDate, bool standard, UserAccount owner, int numberOfLikes, ICollection<DeckItem> deckContent, CardRegions deckRegions, DeckType type) {
        Id = id;
        DeckCode = deckCode;
        DeckName = deckName;
        PostingDate = postingDate;
        Standard = standard;
        Owner = owner is null ? null : new(owner);
        NumberOfLikes = numberOfLikes;
        DeckContent = deckContent.Select(x => new DeckItemDTO(x)).ToList();
        DeckRegions = deckRegions.ToString();
        DeckType = type.ToString();
    }

    public DeckDetailedDTO(Deck deck) :this(deck.Id, deck.DeckCode, deck.DeckName, deck.PostingDate, deck.Standard, deck.OwnerAccount, deck.NumberOfLikes, deck.DeckContent, deck.DeckRegions, deck.Type) { }
}