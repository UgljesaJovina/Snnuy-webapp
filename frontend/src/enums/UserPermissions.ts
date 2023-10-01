enum UserPermissions {
    None = 0,
    SubmitDeck = 1,
    SubmitCustomCard = 1 << 1,
    RateDeck = 1 << 2,
    RateCustomCard = 1 << 3,
    ValidateCustomCard = 1 << 4,
    SetDeckOfTheDay = 1 << 5,
    SetCustomCardOfTheDay = 1 << 6,
    ChangePermissions = 1 << 7,
    Normal = SubmitDeck | SubmitCustomCard | RateDeck | RateCustomCard,
    Moderator = Normal | SetDeckOfTheDay | SetCustomCardOfTheDay | ValidateCustomCard,
    Admin = Moderator | ChangePermissions
}

export { UserPermissions }