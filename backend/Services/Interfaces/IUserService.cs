using Services.DTOs;

namespace Services.Interfaces;

public interface IUserService
{
    Task<AuthenticationResponse> Authenticate(AuthenticationRequest request);
}