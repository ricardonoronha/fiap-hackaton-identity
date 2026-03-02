using AgroSolutions.Identity.DTOs;

namespace AgroSolutions.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}
