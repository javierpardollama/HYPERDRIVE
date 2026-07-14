using Hyperdrive.Main.Application.Queries.DriveItem;
using Hyperdrive.Storage.Application.Profiles;
using Hyperdrive.Storage.Application.ViewModels.Views;
using Hyperdrive.Storage.Domain.Managers;
using MediatR;

namespace Hyperdrive.Storage.Application.Handlers.DriveItem;

public class FindDriveItemBinaryByIdHandler : IRequestHandler<FindDriveItemBinaryByIdQuery, ViewDriveItemBinary>
{
    private readonly IDriveItemBinaryManager _manager;

    public FindDriveItemBinaryByIdHandler(IDriveItemBinaryManager manager)
    {
        _manager = manager;
    }

    public async Task<ViewDriveItemBinary> Handle(FindDriveItemBinaryByIdQuery request, CancellationToken cancellationToken)
    {
        var @item = await _manager.FindDriveItemBinaryById(request.Id);

        return @item.ToViewModel();
    }
}