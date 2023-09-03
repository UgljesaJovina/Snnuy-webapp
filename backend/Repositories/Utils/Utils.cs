using System.ComponentModel;
using Repositories.Models;

namespace Repositories.Utils;

public static class Utils
{
    // public static CustomCard CUSTOM_CARD_OF_THE_DAY;
    // public static Deck DECK_OF_THE_DAY;
    // moved to CustomCardOTD (of the day) and DeckOTD

    public static readonly string CUSTOM_CARD_PATH = @".\public\customCards\";
    public static readonly TimeSpan AUTOMATIC_CARDOTD_DELAY = new(24, 0, 0);
    public static readonly TimeSpan AUTOMATIC_DECKOTD_DELAY = new(24, 0, 0);
    [Description("The maximum size of an uploaded file in bytes")]
    public static readonly ulong MAXIMUM_FILE_SIZE = 2_097_152; // 2 MB
}
