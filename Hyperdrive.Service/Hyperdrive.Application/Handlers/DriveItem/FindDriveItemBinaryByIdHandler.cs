using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.Queries.DriveItem;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;

namespace Hyperdrive.Application.Handlers.DriveItem;

public class FindDriveItemBinaryByIdHandler : IRequestHandler<FindDriveItemBinaryByIdQuery, ViewDriveItemBinary>
{
    private readonly IDriveItemManager _manager;

    public FindDriveItemBinaryByIdHandler(IDriveItemManager manager)
    {
        _manager = manager;
    }

    public async Task<ViewDriveItemBinary> Handle(FindDriveItemBinaryByIdQuery request, CancellationToken cancellationToken)
    {
        var @item = await _manager.FindDriveItemBinaryById(request.Id);

        return @item.ToViewModel();
    }
}