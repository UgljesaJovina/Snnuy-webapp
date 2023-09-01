using System.ComponentModel.DataAnnotations;
using Repositories.Enums;

namespace Repositories.Models;

public class Card
{
    [Key]
    [MaxLength(7)]
    public string CardCode { get; set; }
    [MaxLength(50)]
    public string CardName { get; set; }
    public int ManaCost { get; set; }
    public int AttackPower { get; set; }
    public int HealthValue { get; set; } 
    // public string CardDescription { get; set; }
    public string CardImageLink { get; set; }
    public CardRegions Regions { get; set; }
    public CardTypes Type { get; set; }
    public CardRarity Rarity { get; set; }
}
