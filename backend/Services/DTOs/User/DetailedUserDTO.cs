using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class DetailedUserDTO
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Permissions { get; set; }
    public virtual ICollection<CustomCardDTO> OwnedCards { get; set; } = new List<CustomCardDTO>();
    public virtual ICollection<CustomCardDTO> LikedCards { get; set; } = new List<CustomCardDTO>();
    public virtual ICollection<DeckShortObject> OwnedDecks { get; set; } = new List<DeckShortObject>();
    public virtual ICollection<DeckShortObject> LikedDecks { get; set; } = new List<DeckShortObject>();

    public DetailedUserDTO() { }

    public DetailedUserDTO(Guid id, string username, UserPermissions permissions, ICollection<CustomCard> ownedCards, ICollection<CustomCard> likedCards, ICollection<Deck> ownedDecks, ICollection<Deck> likedDecks) {
        Id = id;
        Username = username;
        Permissions = permissions.ToString();
        OwnedCards = ownedCards.Select(x => new CustomCardDTO(x)).ToList();
        LikedCards = likedCards.Select(x => new CustomCardDTO(x)).ToList();
        OwnedDecks = ownedDecks.Select(x => new DeckShortObject(x)).ToList();
        LikedDecks = likedDecks.Select(x => new DeckShortObject(x)).ToList();
    }

    public DetailedUserDTO(UserAccount account) :this(account.Id, account.Username, account.Permissions, account.OwnedCustomCards, account.LikedCustomCards, account.OwnedDecks, account.LikedDecks) { }
}