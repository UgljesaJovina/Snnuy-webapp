using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class DeckCreationRequest
{
    public string DeckCode { get; set; }   
    public string DeckName { get; set; }
    public UserAccount Owner;
    public DeckType Type { get; set; }

    public Deck GetDeck() {
        return new(DeckCode, DeckName, Owner, Type);
    }
}