using MediatR;

namespace Hyperdrive.Storage.Application.Commands.DriveItem;

public class TargetDriveItemVersionByIdCommand : IRequest
{
    public Guid Id { get; set; }
}