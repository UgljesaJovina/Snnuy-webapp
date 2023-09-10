using System.Text.Json;

List<JSONCards> jsonCards = new();
List<Card> cards = new();
foreach (var json in Directory.GetFiles(@".\JSONs\"))
{
    jsonCards.AddRange(JsonSerializer.Deserialize<List<JSONCards>>(File.ReadAllText(json)).Where(x => x.collectible));
}

Dictionary<string, CardRegions> cardRegions = new() {
            { "Ionia", CardRegions.Ionia },
            { "Noxus", CardRegions.Noxus },
            { "Demacia", CardRegions.Demacia },
            { "PiltoverZaun", CardRegions.PNZ },
            { "BandleCity", CardRegions.Bandle_City },
            { "Freljord", CardRegions.Freljord },
            { "ShadowIsles", CardRegions.Shadow_Isles },
            { "Bilgewater", CardRegions.Bilgewater },
            { "Targon", CardRegions.Targon },
            { "Shurima", CardRegions.Shurima },
            { "Runeterra", CardRegions.Runeterra },
        };

Dictionary<string, CardTypes> cardTypes = new() {
            { "Spell", CardTypes.Spell },
            { "Landmark", CardTypes.Landmark },
            { "Equipment", CardTypes.Equipment },
            { "Unit", CardTypes.Follower },
        };

Dictionary<string, CardRarity> cardRarity = new() {
            { "Common", CardRarity.Common },
            { "Champion", CardRarity.Champion },
            { "Rare", CardRarity.Rare },
            { "Epic", CardRarity.Epic },
        };

foreach (var i in jsonCards)
{
    CardRegions regions = CardRegions.None;
    foreach (var k in i.regionRefs)
        regions |= cardRegions[k];

    CardTypes type = cardTypes[i.type];

    if (cardRarity[i.rarityRef] == CardRarity.Champion) type = CardTypes.Champion;

    CardRarity rarity = cardRarity[i.rarityRef];

    cards.Add(new(i.cardCode, i.name, i.cost, i.attack, i.health, i.assets[0].gameAbsolutePath, regions, type, rarity));
}

Console.ReadLine();