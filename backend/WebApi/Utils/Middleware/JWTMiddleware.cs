using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repositories.Interfaces;
using Repositories.Models;
using Repositories.Utility;

namespace WebApi.Utils.MiddleWare;

public class JWTMiddleware
{
    private readonly RequestDelegate next;
    private readonly AppSettings appSettings;

    public JWTMiddleware(RequestDelegate _next, IOptions<AppSettings> _appSettings) {
        next = _next;
        appSettings = _appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IUserRepo userRepo) { // Ovde ce jos preko DI da se doda IUserServices
        string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token is not null) 
            AttachUserToContext(context, token, userRepo);
        
        await next(context);
    }

    private void AttachUserToContext(HttpContext context, string token, IUserRepo userRepo)
    {
        try {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(appSettings.Secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters() {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
            Guid userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            context.Items["User"] = userRepo.GetById(userId).Result;
        } catch {
            // ako se desi greska, desila se vrv u validaciji tokena --> user nije validan
        }
    }
}