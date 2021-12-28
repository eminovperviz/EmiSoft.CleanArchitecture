using EmiSoft.CleanArchitecture.Application.Interfaces;
using EmiSoft.CleanArchitecture.Application.Models.Config;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmiSoft.CleanArchitecture.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly JwtTokenConfig _jwtTokenConfig;

    public JwtService(IOptions<JwtTokenConfig> jwtTokenConfig)
    {
        _jwtTokenConfig = jwtTokenConfig.Value;
    }
    public string GenerateToken(List<Claim> claims)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtTokenConfig.IssuerSigningKey));

        var accessTokenExpiration = DateTime.UtcNow.AddYears(_jwtTokenConfig.TokenLifeTime);

        var token = new JwtSecurityToken(
            issuer: _jwtTokenConfig.ValidIssuer,
            audience: _jwtTokenConfig.ValidAudience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: accessTokenExpiration,
            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
