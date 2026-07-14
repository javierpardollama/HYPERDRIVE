using Hyperdrive.Storage.Application.ViewModels.Updates;
using Hyperdrive.Storage.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Storage.Application.Commands.DriveItem;

public class UpdateDriveItemNameCommand : IRequest<ViewDriveItem>
{
    public UpdateDriveItemName ViewModel { get; set; }
}