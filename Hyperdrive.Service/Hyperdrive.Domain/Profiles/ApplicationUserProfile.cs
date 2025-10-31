using System.Linq;
using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;

namespace Hyperdrive.Domain.Profiles;

/// <summary>
/// Represents a <see cref="ApplicationUserProfile"/> class.
/// </summary>
public static class ApplicationUserProfile
{
    /// <summary>
    /// Transforms to Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="ApplicationUser"/></param>
    /// <returns>Instance of <see cref="ApplicationUserDto"/></returns>
    public static ApplicationUserDto ToDto(this ApplicationUser @entity)
    {
        return new ApplicationUserDto
        {
            Id = @entity.Id,
            LastModified = @entity.ModifiedAt ?? @entity.CreatedAt,
            ApplicationRoles = [.. @entity.UserRoles.Select(x=>x.Role.ToCatalog())],
            Token = @entity.Tokens
                .OrderByDescending(x=> x.ModifiedAt)
                .LastOrDefault()?
                .ToDto(),
            RefreshToken = @entity.RefreshTokens
                .OrderByDescending(x=> x.ModifiedAt)
                .LastOrDefault()?
                .ToDto(),
            FirstName = @entity.FirstName,
            LastName = @entity.LastName,
            Email = @entity.Email,
            PhoneNumber = @entity.PhoneNumber
        };
    }
    
    /// <summary>
    /// Transforms to Catalog Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="ApplicationUser"/></param>
    /// <returns>Instance of <see cref="CatalogDto"/></returns>
    public static CatalogDto ToCatalog(this ApplicationUser @entity)
    {
        return new CatalogDto
        {
            Id = @entity.Id,
            Name = $"{@entity.FirstName} {@entity.LastName}",
        };
    }
}