namespace Repositories.Models;

public class CustomCardOTD
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public CustomCard Card { get; set; }
    public DateTime SettingDate { get; set; } = DateTime.Now;
    public bool SetAutomatically { get; set; } // u slucaju da prodje vremenski period naveden u Utils/CUSTOM_CARd..., nova karta se bira automatski i ovaj bool se stavlja na true
    public UserAccount? CardSetter { get; set; } // account koji je postavio danasnju kartu

    public CustomCardOTD() { }

    public CustomCardOTD(CustomCard card, bool setAutomatically, UserAccount? cardSetter = null) {
        Card = card;
        SetAutomatically = setAutomatically;
        CardSetter = cardSetter;
    }
}
