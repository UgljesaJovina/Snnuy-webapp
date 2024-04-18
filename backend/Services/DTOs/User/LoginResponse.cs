using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class LoginResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Token { get; set; }
    public UserPermissions Permissions { get; set; }
    public ICollection<Guid> OwnedCards { get; set; }
    public ICollection<Guid> LikedCards { get; set; }
    public ICollection<Guid> OwnedDecks { get; set; }
    public ICollection<Guid> LikedDecks { get; set; }

    public LoginResponse(Guid id, string token, string username, UserPermissions permissions, ICollection<CustomCard> ownedCards, ICollection<CustomCard> likedCards, 
        ICollection<Deck> ownedDecks, ICollection<Deck> likedDecks) 
    {
        Id = id;
        Username = username;
        Token = token;
        Permissions = permissions;
        OwnedCards = ownedCards.Select(x => x.Id).ToList();
        LikedCards = likedCards.Select(x => x.Id).ToList();
        OwnedDecks = ownedDecks.Select(x => x.Id).ToList();
        LikedDecks = likedDecks.Select(x => x.Id).ToList();
    }

    public LoginResponse(UserAccount acc, string token) 
        :this(acc.Id, token, acc.Username, acc.Permissions, acc.OwnedCustomCards, acc.LikedCustomCards, acc.OwnedDecks, acc.LikedDecks) { }
}