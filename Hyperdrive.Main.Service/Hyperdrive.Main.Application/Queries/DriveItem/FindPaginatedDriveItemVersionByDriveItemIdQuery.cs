using Hyperdrive.Main.Application.ViewModels.Filters;
using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Main.Application.Queries.DriveItem;

public class FindPaginatedDriveItemVersionByDriveItemIdQuery : IRequest<ViewPage<ViewDriveItemVersion>>
{
    public FilterPageDriveItemVersion ViewModel { get; set; }
}
