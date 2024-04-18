using System.Text.Json;
using System.Timers;
using Repositories.Enums;
using Repositories.Models;

namespace Repositories.Utility;

public static class Utils
{
    // public static CustomCard CUSTOM_CARD_OF_THE_DAY;
    // public static Deck DECK_OF_THE_DAY;
    // moved to CustomCardOTD (of the day) and DeckOTD

    public static readonly string CUSTOM_CARD_PATH = @".\public\customCards\";
    public static readonly string JSON_CARD_PATH = @".\JSONs\";
    public static readonly TimeSpan AUTOMATIC_CARDOTD_DELAY = new(24, 0, 0);
    public static readonly TimeSpan AUTOMATIC_DECKOTD_DELAY = new(24, 0, 0);
    public static DateTime LAST_CARDOTD_SET { get; set; } = DateTime.Now;
    public static DateTime LAST_DECKOTD_SET { get; set; } = DateTime.Now;
    public static readonly long MAXIMUM_FILE_SIZE = 2_097_152; // 2 MB
    public static readonly ICollection<Card> Cards = new List<Card>();
    public static readonly System.Timers.Timer CardTimer = new(70 * 60 * 1000);
    public static readonly System.Timers.Timer DeckTimer = new(70 * 60 * 1000);

    static Utils()
    {
        List<JSONCards> jsonCards = new();
        foreach (var json in Directory.GetFiles(JSON_CARD_PATH))
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

            bool standard = i.formats.Contains("Standard");

            Cards.Add(new(i.cardCode, i.name, i.cost, i.attack, i.health, i.assets[0].gameAbsolutePath, standard, regions, type, rarity));
        }

        CardTimer.Start();
        DeckTimer.Start();
    }

    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (var i in enumerable)
            action(i);
    }

    public static object GetPropertyValue(this object obj, string propertyName) {
        return obj.GetType().GetProperty(propertyName).GetValue(obj);
    }
}