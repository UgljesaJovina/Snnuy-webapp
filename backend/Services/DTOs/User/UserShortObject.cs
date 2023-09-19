using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class UserShortObject
{
    public Guid Id { get; set; }
    public string Username { get; set; }

    public UserShortObject() { }

    public UserShortObject(Guid id, string username) {
        Id = id;
        Username = username;
    }

    public UserShortObject(UserAccount account) :this(account.Id, account.Username) { }
}