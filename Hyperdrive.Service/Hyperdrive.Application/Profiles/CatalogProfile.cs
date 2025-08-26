using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Dtos;

namespace Hyperdrive.Application.Profiles;

public static class CatalogProfile
{
    public static ViewCatalog ToCatalogViewModel(this CatalogDto @dto)
    {
        return new ViewCatalog
        {
            Id = @dto.Id,
            Name = @dto.Name
        };
    }
}