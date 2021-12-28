using System.Security.Claims;

namespace EmiSoft.CleanArchitecture.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(List<Claim> claims);
}
