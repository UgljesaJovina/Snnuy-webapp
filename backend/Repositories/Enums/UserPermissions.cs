namespace Repositories.Enums;

[Flags]
public enum UserPermissions
{
    None = 0b_0000_0000,
    SubmitDeck = 0b_0000_0001,
    SubmitCustomCard = 0b_0000_0010,
    RateDeck = 0b_0000_0100,
    RateCustomCard = 0b_0000_1000,
    SetDecoOfTheDay = 0b_0001_0000,
    SetCustomCardOfTheDay = 0b_0010_0000,
    NormalAccount = SubmitDeck | SubmitCustomCard | RateDeck | RateCustomCard,
    Moderator = SubmitDeck | SubmitCustomCard | RateDeck | RateCustomCard | SetDecoOfTheDay | SetCustomCardOfTheDay
}