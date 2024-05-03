using System.Text.RegularExpressions;
using Repositories.Enums;
using Repositories.Interfaces;
using Repositories.Models;
using Services.DTOs;
using Services.Interfaces;
using Services.Utility.Hashing;
using Services.Utility.JWT;

namespace Services.Services;

public class UserService(IUserRepo _userRepo, IJwtGenerator _jwtGenerator, IPasswordHasher _passwordHashser) : IUserService
{
    private readonly IUserRepo userRepo = _userRepo;
    private readonly IJwtGenerator jwtGenerator = _jwtGenerator;
    private readonly IPasswordHasher passwordHasher = _passwordHashser;

    readonly string usernameMatch = @"^[a-zA-Z0-9_\.]+$";

    public async Task<LoginResponse> Authenticate(AuthenticationRequest request)
    {
        UserAccount account = await userRepo.GetByUsername(request.Username);

        if (!passwordHasher.Verify(account.HashedPassword, request.Password)) throw new ArgumentException("Password does not match with this username");

        string token = jwtGenerator.GenerateJWTToken(account);
        return new(account, token);
    }

    public async Task<RegistrationResponse> Register(AuthenticationRequest request) {
        if (!Regex.IsMatch(request.Username, usernameMatch)) throw new ArgumentException("The username can contain only letters, numbers, dots and underscores!");
        if (request.Username.Length < 2 || request.Username.Length > 16) throw new ArgumentException("The username can't be longer than 16 characters or shorter than 2!");
        if (request.Password.Length < 8 || request.Password.Length > 40) throw new ArgumentException("The password can't be shorter than 8 characters or longer than 40!");

        string passwordHash = passwordHasher.Hash(request.Password);

        UserAccount account = await userRepo.Create(new UserAccount(request.Username, passwordHash));
        string token = jwtGenerator.GenerateJWTToken(account);
        return new(account, token);
    }

    public async Task Update(UpdateAccountRequest request)
    {
        if (!string.IsNullOrEmpty(request.Password) && request.Password.Length >= 8) request.Password = passwordHasher.Hash(request.Password);

        await userRepo.Update(request.Id, request.GetUserAccount());
    }

    public async Task<ICollection<UserShortObject>> GetAll()
    {
        return (await userRepo.GetAll()).Select(x => new UserShortObject(x)).ToList();
    }

    public async Task Delete(Guid id)
    {
        await userRepo.Delete(id);
    }

    public async Task<DetailedUserDTO> GetById(Guid id)
    {
        return new(await userRepo.GetById(id));
    }

    public async Task ChangeUserPermissions(Guid userId, int permissions)
    {
        (await userRepo.GetById(userId)).Permissions = (UserPermissions)permissions;
    }
}