using MediatR;

namespace Hyperdrive.Storage.Application.Commands.DriveItem;

public class TargetDriveItemVersionByIdCommand : IRequest
{
    public int Id { get; set; }
}