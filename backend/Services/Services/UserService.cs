using Repositories.Enums;
using Repositories.Interfaces;
using Repositories.Models;
using Services.DTOs;
using Services.Interfaces;
using Services.Utility.Hashing;
using Services.Utility.JWT;

namespace Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepo userRepo;
    private readonly IJwtGenerator jwtGenerator;
    private readonly IPasswordHasher passwordHasher;

    public UserService(IUserRepo _userRepo, IJwtGenerator _jwtGenerator, IPasswordHasher _passwordHashser) {
        userRepo = _userRepo;
        jwtGenerator = _jwtGenerator;
        passwordHasher = _passwordHashser;
    }

    public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request)
    {
        string passwordHash = passwordHasher.Hash(request.Password);

        UserAccount account = await userRepo.GetByUsername(request.Username);

        if (!passwordHasher.Verify(account.HashedPassword, request.Password)) throw new KeyNotFoundException("Password does not match with this username");

        string token = jwtGenerator.GenerateJWTToken(account);
        return new(account.Username, account.Permissions, token);
    }

    public async Task<AuthenticationResponse> Register(AuthenticationRequest request) {
        string passwordHash = passwordHasher.Hash(request.Password);

        UserAccount account = await userRepo.Create(new UserAccount(request.Username, passwordHash));
        string token = jwtGenerator.GenerateJWTToken(account);
        return new(account.Username, account.Permissions, token);
    }

    public async Task Update(UpdateAccountRequest request)
    {
        if (!string.IsNullOrEmpty(request.Password)) request.Password = passwordHasher.Hash(request.Password);

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
        return new DetailedUserDTO(await userRepo.GetById(id));
    }

    public async Task ChangeUserPermissions(Guid userId, int permissions)
    {
        (await userRepo.GetById(userId)).Permissions = (UserPermissions)permissions;
    }
}