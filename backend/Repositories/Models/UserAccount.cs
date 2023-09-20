using System.ComponentModel.DataAnnotations;
using Repositories.Enums;

namespace Repositories.Models;

public class UserAccount
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [MaxLength(50)]
    public string Username { get; set; }
    [MaxLength(500)]
    public string HashedPassword { get; set; }
    public UserPermissions Permissions { get; set; }
    public ICollection<CustomCard> OwnedCustomCards { get; set; } = new List<CustomCard>();
    public ICollection<CustomCard> LikedCustomCards { get; set; } = new List<CustomCard>();
    // public ICollection<CustomCardOTD> SetCustomCards { get; set; } = new List<CustomCardOTD>();
    public ICollection<Deck> OwnedDecks { get; set; } = new List<Deck>();
    public ICollection<Deck> LikedDecks { get; set; } = new List<Deck>();
    // public ICollection<DeckOTD> SetDecks { get; set; } = new List<DeckOTD>();

    public UserAccount() {}

    public UserAccount(string username, string password, UserPermissions permissions = UserPermissions.Normal) {
        Username = username;
        HashedPassword = password;
        Permissions = permissions;
    }
}
