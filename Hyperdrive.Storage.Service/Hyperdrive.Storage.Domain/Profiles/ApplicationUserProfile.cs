using Hyperdrive.Storage.Domain.Dtos;
using Hyperdrive.Storage.Domain.Entities;

namespace Hyperdrive.Storage.Domain.Profiles;

/// <summary>
/// Represents a <see cref="ApplicationUserProfile"/> class.
/// </summary>
public static class ApplicationUserProfile
{
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