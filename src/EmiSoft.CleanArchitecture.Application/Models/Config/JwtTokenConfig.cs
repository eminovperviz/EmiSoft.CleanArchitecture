namespace EmiSoft.CleanArchitecture.Application.Models.Config;
public sealed class JwtTokenConfig
{
    public bool ValidateAudience { get; init; }
    public string ValidAudience { get; init; }
    public bool ValidateIssuer { get; init; }
    public string ValidIssuer { get; init; }
    public bool ValidateLifetime { get; init; }
    public bool ValidateIssuerSigningKey { get; init; }
    public string IssuerSigningKey { get; init; }
    public string PasswordSalt { get; init; }
    public int TokenLifeTime { get; init; }
}
