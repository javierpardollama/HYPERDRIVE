using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Queries.DriveItem;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Handlers.DriveItem;

public class FindPaginatedDriveItemByApplicationUserIdHandler : IRequestHandler<FindPaginatedDriveItemByApplicationUserIdQuery, ViewPage<ViewDriveItem>>
{
    public Task<ViewPage<ViewDriveItem>> Handle(FindPaginatedDriveItemByApplicationUserIdQuery request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}