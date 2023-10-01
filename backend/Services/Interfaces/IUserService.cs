using Repositories.Enums;
using Repositories.Models;
using Services.DTOs;

namespace Services.Interfaces;

public interface IUserService
{
    public Task<RegistrationResponse> Register(AuthenticationRequest request);
    public Task<LoginResponse> Authenticate(AuthenticationRequest request);
    public Task<ICollection<UserShortObject>> GetAll();
    public Task<DetailedUserDTO> GetById(Guid id);
    public Task Update(UpdateAccountRequest request);
    public Task Delete(Guid id);
    public Task ChangeUserPermissions(Guid userId, int permissions);
}