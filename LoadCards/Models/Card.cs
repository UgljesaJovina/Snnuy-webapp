
public class Card
{
    public string CardCode { get; set; }
    public string CardName { get; set; }
    public int ManaCost { get; set; }
    public int AttackPower { get; set; }
    public int HealthValue { get; set; } 
    public string CardImageLink { get; set; }
    public CardRegions Regions { get; set; }
    public CardTypes Type { get; set; }
    public CardRarity Rarity { get; set; }

    public Card(string cardCode, string cardName, int manaCost, int attackPower, int healthValue, string cardImageLink, CardRegions regions, CardTypes type, CardRarity rarity)
    {
        CardCode = cardCode;
        CardName = cardName;
        ManaCost = manaCost;
        AttackPower = attackPower;
        HealthValue = healthValue;
        CardImageLink = cardImageLink;
        Regions = regions;
        Type = type;
        Rarity = rarity;
    }
}
