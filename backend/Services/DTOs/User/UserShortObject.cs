using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class UserShortObject
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Permissions { get; set; }

    public UserShortObject() { }

    public UserShortObject(Guid id, string username, UserPermissions permissions) {
        Id = id;
        Username = username;
        Permissions = permissions.ToString();
    }

    public UserShortObject(UserAccount account) :this(account.Id, account.Username, account.Permissions) { }
}