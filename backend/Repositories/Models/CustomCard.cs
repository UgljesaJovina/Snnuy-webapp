using System.ComponentModel.DataAnnotations;
using Repositories.Enums;

namespace Repositories.Models;

public class CustomCard
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [MaxLength(50)]
    public string CardName { get; set; }
    [MaxLength(250)]
    public string EffectText { get; set; }
    public int ManaCost { get; set; }
    public int AttackPower { get; set; }
    public int HealthValue { get; set; }
    [MaxLength(500)]
    public string CardDescription { get; set; }
    [MaxLength(75)]
    public string CardImageName { get; set; }

    public CardTypes Type { get; set; }
    public CustomCardState State { get; set; }

    public UserAccount OwnerAccount { get; set; }
    // public virtual ICollection<Keywords> Keywords { get; set; }
    // public virtual ICollection<CustomKeywords> CustomKeywords { get; set; }
}
