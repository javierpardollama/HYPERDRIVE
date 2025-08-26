using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Dtos;

namespace Hyperdrive.Application.Profiles;

/// <summary>
/// Represents a <see cref="ApplicationRoleProfile"/> class.
/// </summary>
public static class CatalogProfile
{
    /// <summary>
    /// Transforms to ViewModel
    /// </summary>
    /// <param name="dto">Injected <see cref="CatalogDto"/></param>
    /// <returns>Instance of <see cref="ViewCatalog"/></returns>
    public static ViewCatalog ToCatalogViewModel(this CatalogDto @dto)
    {
        return new ViewCatalog
        {
            Id = @dto.Id,
            Name = @dto.Name
        };
    }
}