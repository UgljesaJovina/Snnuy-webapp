namespace Repositories.Enums;

[Flags]
public enum UserPermissions
{
    None = 0b_0000_0000,
    SubmitDeck = 0b_0000_0001,
    SubmitCustomCard = 0b_0000_0010,
    RateDeck = 0b_0000_0100,
    RateCustomCard = 0b_0000_1000,
    ValidateCustomCard = 0b_0001_0000,
    SetDeckOfTheDay = 0b_0010_0000,
    SetCustomCardOfTheDay = 0b_0100_0000,
    ChangePermissions = 0b_1000_0000,
    Normal = SubmitDeck | SubmitCustomCard | RateDeck | RateCustomCard,
    Moderator = Normal | SetDeckOfTheDay | SetCustomCardOfTheDay | ValidateCustomCard,
    Admin = Moderator | ChangePermissions
}