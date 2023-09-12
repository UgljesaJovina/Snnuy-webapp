using Services.DTOs;

namespace Services.Interfaces;

public interface IUserService
{
    public Task<AuthenticationResponse> Authenticate(AuthenticationRequest request);
    public Task<AuthenticationResponse> Register(AuthenticationRequest request);
    public Task Update(UpdateAccountRequest request);
    public Task Delete(Guid id);
}