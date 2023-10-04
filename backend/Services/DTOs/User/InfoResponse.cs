using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class InfoResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public UserPermissions Permissions { get; set; }
    public ICollection<Guid> OwnedCards { get; set; }
    public ICollection<Guid> LikedCards { get; set; }
    public ICollection<Guid> OwnedDecks { get; set; }
    public ICollection<Guid> LikedDecks { get; set; }

    public InfoResponse(Guid id, string username, UserPermissions permissions, ICollection<CustomCard> ownedCards, ICollection<CustomCard> likedCards, 
        ICollection<Deck> ownedDecks, ICollection<Deck> likedDecks) 
    {
        Id = id;
        Username = username;
        Permissions = permissions;
        OwnedCards = ownedCards.Select(x => x.Id).ToList();
        LikedCards = likedCards.Select(x => x.Id).ToList();
        OwnedDecks = ownedDecks.Select(x => x.Id).ToList();
        LikedDecks = likedDecks.Select(x => x.Id).ToList();
    }

    public InfoResponse(UserAccount acc) 
        :this(acc.Id, acc.Username, acc.Permissions, acc.OwnedCustomCards, acc.LikedCustomCards, acc.OwnedDecks, acc.LikedDecks) { }
}