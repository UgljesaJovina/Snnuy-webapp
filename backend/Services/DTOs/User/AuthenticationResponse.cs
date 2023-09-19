using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class AuthenticationResponse
{
    public string Username { get; set; }
    public string Token { get; set; }
    public ICollection<Guid> LikedCards { get; set; }
    public ICollection<Guid> LikedDecks { get; set; }

    public AuthenticationResponse(string username, string token, ICollection<Guid> likedCards, ICollection<Guid> likedDecks) {
        Username = username;
        Token = token;

        LikedCards = likedCards;
        LikedDecks = likedDecks;
    }
}