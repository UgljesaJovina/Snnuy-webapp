using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repositories.Models;
using Repositories.Utility;

namespace Services.Utility.JWT;

public class JwtGenerator(IOptions<AppSettings> _appSettings) : IJwtGenerator
{
    private readonly AppSettings appSettings = _appSettings.Value;

    public string GenerateJWTToken(UserAccount account)
    {
        JwtSecurityTokenHandler tokenHandler = new() { SetDefaultTimesOnTokenCreation = false };
        byte[] key = System.Text.Encoding.ASCII.GetBytes(appSettings.Secret);
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new Claim[] { new("id", account.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddYears(40),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}