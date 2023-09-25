using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class AuthenticationResponse
{
    public string Token { get; set; }

    public AuthenticationResponse(string token) {
        Token = token;
    }
}