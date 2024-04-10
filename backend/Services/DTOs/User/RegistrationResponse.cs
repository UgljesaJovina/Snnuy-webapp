using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class RegistrationResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Token;

    public RegistrationResponse(Guid id, string username, string token) {
        Id = id;
        Username = username;
        Token = token;
    }

    public RegistrationResponse(UserAccount acc, string token)
        :this(acc.Id, acc.Username, token) { }
}