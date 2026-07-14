using Hyperdrive.Main.Application.Queries.DriveItem;
using Hyperdrive.Storage.Application.Profiles;
using Hyperdrive.Storage.Application.ViewModels.Views;
using Hyperdrive.Storage.Domain.Managers;
using MediatR;

namespace Hyperdrive.Storage.Application.Handlers.DriveItem;

public class FindLatestDriveItemBinaryByIdHandler : IRequestHandler<FindLatestDriveItemBinaryByIdQuery, ViewDriveItemBinary>
{
    private readonly IDriveItemBinaryManager _manager;

    public FindLatestDriveItemBinaryByIdHandler(IDriveItemBinaryManager manager)
    {
        _manager = manager;
    }

    public async Task<ViewDriveItemBinary> Handle(FindLatestDriveItemBinaryByIdQuery request, CancellationToken cancellationToken)
    {
        var @item = await _manager.FindLatestDriveItemBinaryById(request.Id);

        return @item.ToViewModel();
    }
}