using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class AuthenticationResponse
{
    public string Username { get; set; }
    public string Token { get; set; }
    public IEnumerable<Guid> LikedCards { get; set; }
    public IEnumerable<Guid> LikedDecks { get; set; }

    public AuthenticationResponse(string username, string token, IEnumerable<Guid> likedCards, IEnumerable<Guid> likedDecks) {
        Username = username;
        Token = token;

        LikedCards = likedCards;
        LikedDecks = likedDecks;
    }
}