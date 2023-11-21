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
    // [MaxLength(250)]
    // public string EffectText { get; set; }
    // public int ManaCost { get; set; }
    // public int AttackPower { get; set; }
    // public int HealthValue { get; set; } 
    // vrednosti se vide na slici karte pa nisu potrebne
    [MaxLength(500)]
    public string CardDescription { get; set; }
    // public string CardImageName { get; set; } koristicu id karte

    public Stream FileSteam;
    // Stream se dobije od IFormFile.GetReadableSteam() il tako nesto, treba da sluzi samo da kad dodje karta moze odmah da se zapise, razmislicu da uklonim polje skroz

    public CardRegions Regions { get; set; }
    public CardTypes Type { get; set; }
    public CustomCardApprovalState State { get; set; }
    public UserAccount? OwnerAccount { get; set; }
    public int NumberOfLikes { get; set; } = 0;
    public ICollection<UserAccount> LikedUsers { get; set; } = new List<UserAccount>();
    // public virtual ICollection<Keywords> Keywords { get; set; }
    // public virtual ICollection<CustomKeywords> CustomKeywords { get; set; }

    public CustomCard() { }

    public CustomCard(string cardName, string cardDescription, CardRegions regions, CardTypes type, CustomCardApprovalState state, UserAccount owner) 
    {
        CardName = cardName;
        CardDescription = cardDescription;
        Regions = regions;
        Type = type;
        State = state;
        OwnerAccount = owner;
    }
}
