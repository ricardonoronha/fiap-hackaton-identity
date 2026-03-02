using System.ComponentModel.DataAnnotations;

namespace AgroSolutions.Identity.DTOs;

public record LoginRequest
{
    [Required(ErrorMessage = "Username is required")]
    public string Email { get; init; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; init; } = string.Empty;
}
