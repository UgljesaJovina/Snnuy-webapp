using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repositories.Models;
using Repositories.Utility;

namespace Services.Utils.JWT;

public class JwtGenerator : IJwtGenerator
{
    private AppSettings appSettings;

    public JwtGenerator(IOptions<AppSettings> _appSettings) {
        appSettings = _appSettings.Value;
    }

    public string GenerateJWTToken(UserAccount account)
    {
        JwtSecurityTokenHandler tokenHandler = new() { SetDefaultTimesOnTokenCreation = false };
        byte[] key = System.Text.Encoding.ASCII.GetBytes(appSettings.Secret);
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", account.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddYears(40),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}