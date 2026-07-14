using Hyperdrive.Storage.Application.ViewModels.Additions;
using Hyperdrive.Storage.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Storage.Application.Commands.DriveItem;

public class AddDriveItemCommand : IRequest<ViewDriveItem>
{
    public AddDriveItem ViewModel { get; set; }
}