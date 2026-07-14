using Hyperdrive.Storage.Application.ViewModels.Updates;
using Hyperdrive.Storage.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Storage.Application.Commands.DriveItem;

public class UpdateDriveItemSharedWithCommand : IRequest<ViewDriveItem>
{
    public UpdateDriveItemSharedWith ViewModel { get; set; }
}