using Repositories.Enums;
using Repositories.Models;

namespace Services.DTOs;

public class UpdateAccountRequest
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }

    public UserAccount GetUserAccount() {
        return new UserAccount() { 
            Id = Id,
            Username = UserName, 
            HashedPassword = Password
        };
    }
}
