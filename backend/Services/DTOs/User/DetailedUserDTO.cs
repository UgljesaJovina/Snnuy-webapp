using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class DetailedUserDTO
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Permissions { get; set; }
    public ICollection<CustomCardDTO> OwnedCards { get; set; }
    public ICollection<CustomCardDTO> LikedCards { get; set; }
    public ICollection<DeckDTO> OwnedDecks { get; set; }
    public ICollection<DeckDTO> LikedDecks { get; set; }

    public DetailedUserDTO() { }

    public DetailedUserDTO(Guid id, string username, UserPermissions permissions, ICollection<CustomCard> ownedCards, ICollection<CustomCard> likedCards, ICollection<Deck> ownedDecks, ICollection<Deck> likedDecks) {
        Id = id;
        Username = username;
        Permissions = permissions.ToString();
        OwnedCards = ownedCards.Select(x => new CustomCardDTO(x)).ToList();
        LikedCards = likedCards.Select(x => new CustomCardDTO(x)).ToList();
        OwnedDecks = ownedDecks.Select(x => new DeckDTO(x)).ToList();
        LikedDecks = likedDecks.Select(x => new DeckDTO(x)).ToList();
    }

    public DetailedUserDTO(UserAccount account) :this(account.Id, account.Username, account.Permissions, account.OwnedCustomCards, account.LikedCustomCards, account.OwnedDecks, account.LikedDecks) { }
}