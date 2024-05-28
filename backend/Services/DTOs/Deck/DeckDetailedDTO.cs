using Repositories.Enums;
using Repositories.Models;
using Repositories.Utility;

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
    public DeckType DeckType { get; set; } = type;
    public CardRegions DeckRegions { get; set; } = deckRegions;
    public ICollection<Card> Champions { get; set; } = deckContent.Where(x => x.Card.Rarity == CardRarity.Champion).Select(x => x.Card).ToList();
    public Dictionary<int, int> RegionCardCount
    {
        get
        {
            Dictionary<CardRegions, int> dict = [];
            List<Card> trackedCards = [];

            for (int i = 1; i < (int)DeckRegions; i <<= 1)
                if ((i & (int)DeckRegions) != 0) dict.Add((CardRegions)i, 0);

            foreach (var i in dict)
                foreach (var k in deckContent)
                    if ((i.Key & k.Card.Regions) != 0 && !trackedCards.Contains(k.Card))
                    {
                        dict[i.Key] += k.Count;
                        trackedCards.Add(k.Card);
                    }

            if (dict.Count > 2 && dict.ContainsKey(CardRegions.Runeterra))
            {
                CardRegions biggestRegion = dict.MaxBy(x => x.Value).Key;
                dict.ForEach(x =>
                {
                    if (((biggestRegion | CardRegions.Runeterra) & x.Key) == 0)
                    {
                        dict[CardRegions.Runeterra] += dict[x.Key];
                        dict[x.Key] = 0;
                    }
                });
                dict.Where(x => x.Value == 0).ForEach(x => dict.Remove(x.Key));
            }

            return dict.ToDictionary(x => (int)x.Key, x => x.Value);
        }
    }
    public ICollection<DeckItemDTO> DeckContent { get; set; } = deckContent.Select(x => new DeckItemDTO(x)).ToList();
    public int NumberOfLikes { get; set; } = numberOfLikes;
    public int DeckCost { get { return DeckContent.Select(x => x.Rarity).Sum(x => (int)x); }}

    public DeckDetailedDTO(Deck deck) :this(deck.Id, deck.DeckCode, deck.DeckName, deck.PostingDate, deck.Standard,
        deck.OwnerAccount, deck.NumberOfLikes, deck.DeckContent, deck.DeckRegions, deck.Type) { }
}