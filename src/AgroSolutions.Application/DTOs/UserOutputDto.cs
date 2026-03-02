namespace AgroSolutions.Identity.DTOs;

public record UserOutputDto
{
    public string Email { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
}
