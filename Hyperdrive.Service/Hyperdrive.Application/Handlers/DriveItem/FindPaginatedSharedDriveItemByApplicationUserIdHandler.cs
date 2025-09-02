using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.Queries.DriveItem;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;

namespace Hyperdrive.Application.Handlers.DriveItem;

public class FindPaginatedSharedDriveItemByApplicationUserIdHandler : IRequestHandler<FindPaginatedSharedDriveItemByApplicationUserIdQuery, ViewPage<ViewDriveItem>>
{
    private readonly IDriveItemManager _manager;

    public FindPaginatedSharedDriveItemByApplicationUserIdHandler(IDriveItemManager manager)
    {
        _manager = manager;
    }
    
    public async Task<ViewPage<ViewDriveItem>> Handle(FindPaginatedSharedDriveItemByApplicationUserIdQuery request, CancellationToken cancellationToken)
    {
        var @page = await _manager.FindPaginatedSharedDriveItemWithApplicationUserId(
            request.ViewModel.Index, 
            request.ViewModel.Size,
            request.ViewModel.ApplicationUserId
        ); 
        
        return @page.ToPageViewModel();
    }
}