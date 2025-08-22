using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;

namespace Hyperdrive.Domain.Profiles;

public static class ApplicationUserRefreshTokenProfile
{
    public static TokenDto ToDto(this ApplicationUserRefreshToken @entity)
    {
        return new TokenDto
        {
           IssuedAt = @entity.LastModified,
           LoginProvider = @entity.LoginProvider,
           Value = @entity.Value
        };
    }
}