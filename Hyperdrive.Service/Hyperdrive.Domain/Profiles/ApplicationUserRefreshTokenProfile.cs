using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;

namespace Hyperdrive.Domain.Profiles;

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
           LoginProvider = @entity.LoginProvider,
           Value = @entity.Value
        };
    }
}