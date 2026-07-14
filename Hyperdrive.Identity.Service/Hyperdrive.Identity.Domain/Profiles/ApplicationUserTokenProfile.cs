using Hyperdrive.Identity.Domain.Dtos;
using Hyperdrive.Identity.Domain.Entities;

namespace Hyperdrive.Identity.Domain.Profiles;

/// <summary>
/// Represents a <see cref="ApplicationUserTokenProfile"/> class.
/// </summary>
public static class ApplicationUserTokenProfile
{
    /// <summary>
    /// Transforms to Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="ApplicationUserToken"/></param>
    /// <returns>Instance of <see cref="TokenDto"/></returns>
    public static TokenDto ToDto(this ApplicationUserToken @entity)
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