using System.ComponentModel.DataAnnotations;
using Repositories.Enums;

namespace Repositories.Models;

public class CustomCardOTD 
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public CustomCard Card { get; set; }
    public DateTime SettingDate { get; set; } = DateTime.Now;
    public bool SetAutomatically { get { return CardSetter is null; } } 
    public UserAccount? CardSetter { get; set; } 

    public CustomCardOTD() { }

    public CustomCardOTD(CustomCard card, UserAccount? cardSetter = null) 
    {
        Card = card;
        CardSetter = cardSetter;
    }
}
