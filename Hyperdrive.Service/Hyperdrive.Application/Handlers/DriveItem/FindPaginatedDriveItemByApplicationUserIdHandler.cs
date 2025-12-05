using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.Queries.DriveItem;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Application.Handlers.DriveItem;

public class FindPaginatedDriveItemByApplicationUserIdHandler : IRequestHandler<FindPaginatedDriveItemByApplicationUserIdQuery, ViewPage<ViewDriveItem>>
{
    private readonly IDriveItemManager _manager;

    public FindPaginatedDriveItemByApplicationUserIdHandler(IDriveItemManager manager)
    {
        _manager = manager;
    }
    
    public async Task<ViewPage<ViewDriveItem>> Handle(FindPaginatedDriveItemByApplicationUserIdQuery request, CancellationToken cancellationToken)
    {
        var @page = await _manager.FindPaginatedDriveItemByApplicationUserId(
            request.ViewModel.Index, 
            request.ViewModel.Size,
            request.ViewModel.ApplicationUserId,
            request.ViewModel.ParentId
            ); 
        
        return @page.ToPageViewModel();
    }
}