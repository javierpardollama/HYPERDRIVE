using System.Linq;
using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;

namespace Hyperdrive.Domain.Profiles;

public static class ApplicationUserProfile
{
    public static ApplicationUserDto ToDto(this ApplicationUser @entity)
    {
        return new ApplicationUserDto
        {
            Id = @entity.Id,
            LastModified = @entity.LastModified,
            ApplicationRoles = @entity.ApplicationUserRoles
                .Select(x=>x.ApplicationRole.ToCatalog())
                .ToList(),
            Token = @entity.ApplicationUserTokens
                .OrderByDescending(x=> x.LastModified)
                .LastOrDefault()
                .ToDto(),
            RefreshToken = @entity.ApplicationUserRefreshTokens
                .OrderByDescending(x=> x.LastModified)
                .LastOrDefault()
                .ToDto(),
            FirstName = @entity.FirstName,
            LastName = @entity.LastName,
            Email = @entity.Email,
            PhoneNumber = @entity.PhoneNumber
        };
    }
    
    public static CatalogDto ToCatalog(this ApplicationUser @entity)
    {
        return new CatalogDto
        {
            Id = @entity.Id,
            Name = $"{@entity.FirstName} {@entity.LastName}",
        };
    }
}