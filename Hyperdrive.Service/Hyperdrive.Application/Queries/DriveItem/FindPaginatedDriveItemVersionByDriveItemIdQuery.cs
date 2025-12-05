using Hyperdrive.Application.ViewModels.Filters;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Queries.DriveItem;

public class FindPaginatedDriveItemVersionByDriveItemIdQuery : IRequest<ViewPage<ViewDriveItemVersion>>
{
    public FilterPageDriveItemVersion ViewModel { get; set; }
}
