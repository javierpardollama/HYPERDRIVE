using Hyperdrive.Application.ViewModels.Filters;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Queries.DriveItem;

public class FindPaginatedDriveItemByApplicationUserIdQuery : IRequest<ViewPage<ViewDriveItem>>
{
    public FilterPageDriveItem ViewModel { get; set; }
}