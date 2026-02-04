using Hyperdrive.Main.Application.ViewModels.Filters;
using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Main.Application.Queries.DriveItem;

public class FindPaginatedSharedDriveItemByApplicationUserIdQuery : IRequest<ViewPage<ViewDriveItem>>
{
    public FilterPageDriveItem ViewModel { get; set; }
}