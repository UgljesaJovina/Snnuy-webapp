using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class DeckItemDTO
{
    public int Count { get; set; }
    public string CardCode { get; set; }
    public string CardName { get; set; }
    public int ManaCost { get; set; }
    public int AttackPower { get; set; }
    public int HealthValue { get; set; } 
    public string CardImageLink { get; set; }
    public string CardBackgroundLink { get; set; }
    public bool Standard { get; set; }
    public CardRegions Regions { get; set; }
    public CardTypes Type { get; set; }
    public CardRarity Rarity { get; set; }

    public DeckItemDTO() { }

    public DeckItemDTO(int count, string cardCode, string cardName, int manaCost, int attackPower, int healthValue, string cardImageLink,
        string cardBackgroundLink, bool standard, CardRegions regions, CardTypes type, CardRarity rarity) 
    {
        Count = count;
        CardCode = cardCode;
        CardName = cardName;
        ManaCost = manaCost;
        AttackPower = attackPower;
        HealthValue = healthValue;
        CardImageLink = cardImageLink;
        CardBackgroundLink = cardBackgroundLink;
        Standard = standard;
        Regions = regions;
        Type = type;
        Rarity = rarity;
    }

    public DeckItemDTO(DeckItem item) :this(item.Count, item.Card.CardCode, item.Card.CardName, item.Card.ManaCost, item.Card.AttackPower,
        item.Card.HealthValue, item.Card.CardImageLink, item.Card.CardBackgroundLink, item.Card.Standard,
        item.Card.Regions, item.Card.Type, item.Card.Rarity) { }
}