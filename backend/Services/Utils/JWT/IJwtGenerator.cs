using Repositories.Models;

namespace Services.Utility.JWT;

public interface IJwtGenerator
{
    public string GenerateJWTToken(UserAccount account);
}