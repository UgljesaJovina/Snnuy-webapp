using Repositories.Models;

namespace Services.Utils.JWT;

public interface IJwtGenerator
{
    public string GenerateJWTToken(UserAccount account);
}