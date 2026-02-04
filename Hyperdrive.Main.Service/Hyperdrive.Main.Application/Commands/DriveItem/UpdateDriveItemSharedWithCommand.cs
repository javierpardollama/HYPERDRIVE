using Hyperdrive.Main.Application.ViewModels.Updates;
using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Main.Application.Commands.DriveItem;

public class UpdateDriveItemSharedWithCommand : IRequest<ViewDriveItem>
{
    public UpdateDriveItemSharedWith ViewModel { get; set; }
}