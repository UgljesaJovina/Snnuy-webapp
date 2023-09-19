using System.Text.Json.Serialization;
using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class CustomCardCreationRequset
{
    public string CardName { get; set; }
    public string CardDescription { get; set; }
    public CardTypes CardType { get; set; } 
    public Stream DataStream;
    public UserAccount Owner;

    public CustomCard GetCustomCard() {
        return new(CardName, DateTime.Now, CardDescription, DataStream, CardType, CustomCardApprovalState.Pending, Owner);
    }
}