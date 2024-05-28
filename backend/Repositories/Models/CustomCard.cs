using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repositories.Enums;

namespace Repositories.Models;

public class CustomCard
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [MaxLength(50)]
    public string CardName { get; set; }
    public DateTime PostingDate { get; set; } = DateTime.Now;
    [MaxLength(500)]
    public string CardDescription { get; set; }
    public string CardImageName { get; set; }
    public CardRegions Regions { get; set; }
    public CardTypes Type { get; set; }
    public CustomCardApprovalState ApprovalState { get; set; }
    public UserAccount? OwnerAccount { get; set; }

    [NotMapped]
    public int NumberOfLikes { get { return LikedUsers.Count; } }
    public ICollection<UserAccount> LikedUsers { get; set; } = [];
    public CustomCard() { }

    public CustomCard(string cardName, string cardDescription, string imagePath, CardRegions regions, CardTypes type, CustomCardApprovalState state, UserAccount owner) 
    {
        CardName = cardName;
        CardDescription = cardDescription;
        CardImageName = imagePath;
        Regions = regions;
        Type = type;
        ApprovalState = state;
        OwnerAccount = owner;
    }
}

public record CardLikeRecord(Guid Id, bool Liked, int NumberOfLikes);