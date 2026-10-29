using Hyperdrive.Storage.Application.ViewModels.Filters;
using Hyperdrive.Storage.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Storage.Application.Queries.DriveItem;

public class FindPaginatedDriveItemVersionByDriveItemIdQuery : IRequest<ViewPage<ViewDriveItemVersion>>
{
    public FilterPageDriveItemVersion ViewModel { get; set; }
}
