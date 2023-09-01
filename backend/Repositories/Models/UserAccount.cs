using System.ComponentModel.DataAnnotations;
using Repositories.Enums;

namespace Repositories.Models;

public class UserAccount
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [MaxLength(25)]
    public string UserName { get; set; }
    [MaxLength(25)]
    public string Password { get; set; }
    public UserPermissions Permissions { get; set; }
    public virtual ICollection<CustomCard> CustomCards { get; set; }
    // public virtual ICollection<Deck> Decks { get; set; }
}
