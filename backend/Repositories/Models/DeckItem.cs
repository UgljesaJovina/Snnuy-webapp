namespace Repositories.Models;

public class DeckItem
{
    public Guid Id { get; set; }
    public Card Card { get; set; }
    public int Count { get; set; }
}