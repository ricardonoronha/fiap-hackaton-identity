using AgroSolutions.Application.Interfaces;
using AgroSolutions.Identity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroSolutions.Identity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Realiza login e retorna token JWT
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.LoginAsync(request);

        if (result == null)
            return Unauthorized(new { message = "Email or password invalid" });

        return Ok(result);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] UserInputDto request, [FromServices] IUserService userService)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await userService.AddUserAsync(request);
        return Ok(new { message = "User registered" });
    }

}
