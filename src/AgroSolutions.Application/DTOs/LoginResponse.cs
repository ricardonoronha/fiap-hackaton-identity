namespace AgroSolutions.Identity.DTOs;

public record LoginResponse
{
    public string Token { get; init; } = string.Empty;
    public string TokenType { get; init; } = "Bearer";
    public int ExpiresIn { get; init; }
    public DateTime ExpiresAt { get; init; }
    public UserOutputDto User { get; init; } = null!;
}
