using Hyperdrive.Application.ViewModels.Additions;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Commands.DriveItem;

public class AddDriveItemCommand : IRequest<ViewDriveItem>
{
    public AddDriveItem ViewModel { get; set; }
}