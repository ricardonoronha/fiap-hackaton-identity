using AgroSolutions.Identity.DTOs;
using AgroSolutions.Identity.Models;

namespace AgroSolutions.Application.Interfaces;

public interface ITokenService
{
    LoginResponse GenerateToken(UserOutputDto user);
}
