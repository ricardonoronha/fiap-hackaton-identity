using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AgroSolutions.Application.DTOs;
using AgroSolutions.Application.Interfaces;
using AgroSolutions.Identity.DTOs;
using AgroSolutions.Identity.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AgroSolutions.Identity.Services;

public class TokenService : ITokenService
{
    private readonly JwtOptions _options;

    public TokenService(IOptions<JwtOptions> options) => _options = options.Value;

    public LoginResponse GenerateToken(UserOutputDto user)
    {
        var secretKey = _options.Secret
            ?? throw new InvalidOperationException("JWT Secret não configurado");
        
        var issuer = _options.Issuer ?? "AgroSolutions";
        var audience = _options.Audience ?? "AgroSolutions.Api";
        var expirationMinutes = int.Parse(_options.ExpirationMinutes ?? "60");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var expiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials
        );

        return new LoginResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            TokenType = "Bearer",
            ExpiresIn = expirationMinutes * 60,
            ExpiresAt = expiresAt,
            User = new UserOutputDto
            {
                Email = user.Email,
                Name = user.Name,
            }
        };
    }
}
