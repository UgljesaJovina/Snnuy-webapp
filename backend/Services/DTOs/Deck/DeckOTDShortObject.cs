using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class DeckOTDShortObject
{
    public Guid Id { get; set; }
    public string DeckCode { get; set; }
    public string DeckName { get; set; }
    public bool Standard { get; set; }
    public UserShortObject Owner { get; set; }
    public int NumberOfLikes { get; set; }
    public string DeckType { get; set; }

    public DeckOTDShortObject() { }

    public DeckOTDShortObject(Guid id, string deckCode, string deckName, bool standard, UserAccount owner, int numberOfLikes, DeckType deckType) {
        Id = id;
        DeckCode = deckCode;
        DeckName = deckName;
        Standard = standard;
        Owner = new(owner);
        NumberOfLikes = numberOfLikes;
        DeckType = deckType.ToString();
    }
}