using System.ComponentModel.DataAnnotations;
using Repositories.Enums;

namespace Repositories.Models;

public class UserAccount
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [MaxLength(25)]
    public string Username { get; set; }
    [MaxLength(25)]
    public string Password { get; set; }
    public UserPermissions Permissions { get; set; }
    public virtual ICollection<CustomCard> OwnedCustomCards { get; set; }
    public virtual ICollection<CustomCard> LikedCustomCards { get; set; }
    public virtual ICollection<Deck> OwnedDecks { get; set; }
    public virtual ICollection<Deck> LikedDecks { get; set; }

    public UserAccount() {}

    public UserAccount(string username, string password) {
        Username = username;
        Password = password;
    }
}
