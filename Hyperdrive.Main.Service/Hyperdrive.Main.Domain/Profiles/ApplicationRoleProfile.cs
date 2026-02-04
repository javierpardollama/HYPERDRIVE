using Hyperdrive.Main.Domain.Dtos;
using Hyperdrive.Main.Domain.Entities;

namespace Hyperdrive.Main.Domain.Profiles;

/// <summary>
/// Represents a <see cref="ApplicationRoleDto"/> class.
/// </summary>
public static class ApplicationRoleProfile
{
    /// <summary>
    /// Transforms to Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="ApplicationRole"/></param>
    /// <returns>Instance of <see cref="ApplicationRoleDto"/></returns>
    public static ApplicationRoleDto ToDto(this ApplicationRole @entity)
    {
        return new ApplicationRoleDto
        {
            Id = @entity.Id,
            Name = @entity.Name,
            ImageUri = @entity.ImageUri,
            LastModified = @entity.ModifiedAt ?? @entity.CreatedAt
        };
    }

    /// <summary>
    /// Transforms to Catalog Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="ApplicationRole"/></param>
    /// <returns>Instance of <see cref="CatalogDto"/></returns>
    public static CatalogDto ToCatalog(this ApplicationRole @entity)
    {
        return new CatalogDto
        {
            Id = @entity.Id,
            Name = @entity.Name
        };
    }
}