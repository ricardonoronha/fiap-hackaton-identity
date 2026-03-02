using AgroSolutions.Application.Interfaces;
using AgroSolutions.Identity.Domain.Interfaces;
using AgroSolutions.Identity.DTOs;
using AgroSolutions.Identity.Models;

namespace AgroSolutions.Identity.Services;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IUserService userService;

    public AuthService(ITokenService tokenService, IUserService userService)
    {
        _tokenService = tokenService;
        this.userService = userService;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var user = await userService.GetByCredentials(request.Email, request.Password);

        if (user == null)
            return null;

        return _tokenService.GenerateToken(user);
    }
}
