using System.ComponentModel.DataAnnotations;
using Repositories.Enums;

namespace Repositories.Models;

public class Deck
{
    [Key]
    public string DeckCode { get; set; }
    [MaxLength(50)]
    public string DeckName { get; set; }
    public DateTime PostingDate { get; set; }
    public bool Eternal { get; set; }
    public UserAccount OwnerAccount { get; set; }
    public virtual ICollection<UserAccount> LikedUsers { get; set; }
    public int UpVotes { get; set; }
    public ICollection<DeckItem> DeckContent { get; set; }
    public DeckType Type { get; set; } 
}