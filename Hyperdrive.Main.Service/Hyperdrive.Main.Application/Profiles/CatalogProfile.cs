using Hyperdrive.Main.Application.ViewModels.Views;
using Hyperdrive.Main.Domain.Dtos;

namespace Hyperdrive.Main.Application.Profiles;

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
    public static ViewCatalog ToViewModel(this CatalogDto @dto)
    {
        return new ViewCatalog
        {
            Id = @dto.Id,
            Name = @dto.Name
        };
    }
}