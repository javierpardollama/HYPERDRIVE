using Hyperdrive.Application.ViewModels.Updates;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Commands.DriveItem;

public class UpdateDriveItemSharedWithCommand : IRequest<ViewDriveItem>
{
    public UpdateDriveItemSharedWith ViewModel { get; set; }
}