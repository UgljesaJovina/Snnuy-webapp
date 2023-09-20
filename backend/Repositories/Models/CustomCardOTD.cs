using Repositories.Enums;

namespace Repositories.Models;

public class CustomCardOTD : CustomCard
{
    public DateTime SettingDate { get; set; } = DateTime.Now;
    public bool SetAutomatically { get { return CardSetter is null; } } // u slucaju da prodje vremenski period naveden u Utils/CUSTOM_CARD..., nova karta se bira automatski i ovaj bool se stavlja na true
    public UserAccount? CardSetter { get; set; } // account koji je postavio danasnju kartu

    public CustomCardOTD() { }

    public CustomCardOTD(string cardName, string cardDescription, CardTypes type, 
        CustomCardApprovalState state, UserAccount owner, UserAccount? cardSetter = null) 
        :base(cardName, cardDescription, type, state, owner)
    {
        CardSetter = cardSetter;
    }

    public CustomCardOTD(CustomCard card, UserAccount? cardSetter = null)
    :this(card.CardName, card.CardDescription, card.Type, card.State, card.OwnerAccount, cardSetter) { }
}
