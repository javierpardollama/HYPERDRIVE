using MediatR;

namespace Hyperdrive.Application.Commands.DriveItem;

public class TargetDriveItemVersionByIdCommand : IRequest
{
    public int Id { get; set; }
}