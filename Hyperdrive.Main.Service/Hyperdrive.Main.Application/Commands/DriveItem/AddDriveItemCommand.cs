using Hyperdrive.Main.Application.ViewModels.Additions;
using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Main.Application.Commands.DriveItem;

public class AddDriveItemCommand : IRequest<ViewDriveItem>
{
    public AddDriveItem ViewModel { get; set; }
}