using System.Text.Json;

List<JSONCards> jsonCards = new();
List<Card> cards = new();

foreach (var json in Directory.GetFiles(@".\JSONs\")) {
    jsonCards.AddRange(JsonSerializer.Deserialize<List<JSONCards>>(File.ReadAllText(json)).Where(x => x.collectible));
}

Dictionary<string, CardRegions> regions = new() {
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
    { "Runterra", CardRegions.Runterra },
};

Dictionary<string, CardTypes> cardTypes = new() {
    { "Spell", CardTypes.Spell },
    { "Landmark", CardTypes.Landmark },
    { "Equipment", CardTypes.Equipment },
    { "Unit", CardTypes.Unit },
};

Dictionary<string, CardRarity> cardRarity = new() {
    { "Common", CardRarity.Common },
    { "Champion", CardRarity.Champion },
    { "Rare", CardRarity.Rare },
    { "Epic", CardRarity.Epic },
};

foreach (var i in jsonCards)
{
    cards.Add(new());
}

using (var ctx = new DataContext()) {

}

