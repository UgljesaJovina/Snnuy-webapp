using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class AuthenticationResponse
{
    public string Username { get; set; }
    public UserPermissions Permissions { get; set; }
    public string Token { get; set; }

    public AuthenticationResponse(string username, UserPermissions permissions, string token) {
        Username = username;
        Permissions = permissions;
        Token = token;
    }
}