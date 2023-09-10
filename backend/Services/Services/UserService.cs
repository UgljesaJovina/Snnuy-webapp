using System.IdentityModel.Tokens.Jwt;
using Repositories.Utility;
using Repositories.Interfaces;
using Repositories.Models;
using Services.DTOs;
using Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepo userRepo;
    private readonly AppSettings appSettings;

    public UserService(IUserRepo _userRepo, IOptions<AppSettings> _appSettings) {
        userRepo = _userRepo;
        appSettings = _appSettings.Value;
    }

    public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request)
    {
        UserAccount account = await userRepo.Authenticate(request.Username, request.Password);
        string token = GenerateJWTToken(account);
        return new(account.Username, account.Permissions, token);
    }

    private string GenerateJWTToken(UserAccount account)
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