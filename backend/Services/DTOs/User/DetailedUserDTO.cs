using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class DetailedUserDTO
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public UserPermissions Permissions { get; set; }
    public ICollection<Guid> OwnedCards { get; set; }
    public ICollection<Guid> LikedCards { get; set; }
    public ICollection<Guid> OwnedDecks { get; set; }
    public ICollection<Guid> LikedDecks { get; set; }

    public DetailedUserDTO() { }

    public DetailedUserDTO(Guid id, string username, UserPermissions permissions, ICollection<CustomCard> ownedCards, ICollection<CustomCard> likedCards, 
        ICollection<Deck> ownedDecks, ICollection<Deck> likedDecks) 
    {
        Id = id;
        Username = username;
        Permissions = permissions;
        
        // foreach (UserPermissions permission in Enum.GetValues(typeof(UserPermissions)))
        // {
        //     if ((permission & permissions) != 0 && (permission & (permission - 1)) == 0) // da li je permission samo jedan a ne kombinacija (normal, mod, admin)
        //         Permissions.Add(permission.ToString());
        // }

        OwnedCards = ownedCards.Select(x => x.Id).ToList();
        LikedCards = likedCards.Select(x => x.Id).ToList();
        OwnedDecks = ownedDecks.Select(x => x.Id).ToList();
        LikedDecks = likedDecks.Select(x => x.Id).ToList();
    }

    public DetailedUserDTO(UserAccount account) 
        :this(account.Id, account.Username, account.Permissions, account.OwnedCustomCards, account.LikedCustomCards, account.OwnedDecks, account.LikedDecks) { }
}