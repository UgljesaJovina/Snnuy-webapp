using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class DeckItemDTO
{
    public int Count { get; set; }
    public string CardName { get; set; }
    public int ManaCost { get; set; }
    public int AttackPower { get; set; }
    public int HealthValue { get; set; } 
    public string CardImageLink { get; set; }
    public bool Standard { get; set; }
    public CardRegions Regions { get; set; }
    public CardTypes Type { get; set; }
    public CardRarity Rarity { get; set; }

    public DeckItemDTO() { }

    public DeckItemDTO(int count, string cardName, int manaCost, int attackPower, int healthValue, string cardImageLink, bool standard, CardRegions regions, CardTypes type, CardRarity rarity) {
        Count = count;
        CardName = cardName;
        ManaCost = manaCost;
        AttackPower = attackPower;
        HealthValue = healthValue;
        CardImageLink = cardImageLink;
        Standard = standard;
        Regions = regions;
        Type = type;
        Rarity = rarity;
    }

    public DeckItemDTO(DeckItem item) :this(item.Count, item.Card.CardName, item.Card.ManaCost, item.Card.AttackPower, item.Card.HealthValue, item.Card.CardImageLink,
        item.Card.Standard, item.Card.Regions, item.Card.Type, item.Card.Rarity) { }
}