using Repositories.Enums;

namespace Repositories.Models;

public class Card(string cardCode, string cardName, int manaCost, int attackPower, int healthValue, string cardImageLink, string cardBackgroundLink,
    bool standard, CardRegions regions, CardTypes type, CardRarity rarity)
{
    public string CardCode { get; set; } = cardCode;
    public string CardName { get; set; } = cardName;
    public int ManaCost { get; set; } = manaCost;
    public int AttackPower { get; set; } = attackPower;
    public int HealthValue { get; set; } = healthValue;
    public string CardImageLink { get; set; } = cardImageLink;
    public string CardBackgroundLink { get; set; } = cardBackgroundLink;
    public bool Standard { get; set; } = standard;
    public CardRegions Regions { get; set; } = regions;
    public CardTypes Type { get; set; } = type;
    public CardRarity Rarity { get; set; } = rarity;
}
