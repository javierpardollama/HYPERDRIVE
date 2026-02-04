using Hyperdrive.Main.Domain.Dtos;
using Hyperdrive.Main.Domain.Entities;

namespace Hyperdrive.Main.Domain.Profiles;

/// <summary>
/// Represents a <see cref="ApplicationUserRefreshTokenProfile"/> class.
/// </summary>
public static class ApplicationUserRefreshTokenProfile
{
    /// <summary>
    /// Transforms to Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="ApplicationUserRefreshToken"/></param>
    /// <returns>Instance of <see cref="TokenDto"/></returns>
    public static TokenDto ToDto(this ApplicationUserRefreshToken @entity)
    {
        return new TokenDto
        {
            IssuedAt = @entity.CreatedAt,
            ExpiresAt = @entity.ExpiresAt,
            LoginProvider = @entity.LoginProvider,
            Value = @entity.Value
        };
    }
}