using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;

namespace Hyperdrive.Domain.Profiles;

public static class ApplicationUserTokenProfile
{
    public static TokenDto ToDto(this ApplicationUserToken @entity)
    {
        return new TokenDto
        {
            IssuedAt = @entity.LastModified,
            LoginProvider = @entity.LoginProvider,
            Value = @entity.Value
        };
    }
}