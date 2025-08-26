using Hyperdrive.Application.ViewModels.Updates;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Commands.DriveItem;

public class UpdateDriveItemCommand : IRequest<ViewDriveItem>
{
    public UpdateDriveItem ViewModel { get; set; }
}