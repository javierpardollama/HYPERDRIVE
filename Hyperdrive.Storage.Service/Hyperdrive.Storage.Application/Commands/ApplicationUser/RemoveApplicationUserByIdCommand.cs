using MediatR;

namespace Hyperdrive.Storage.Application.Commands.ApplicationUser;

public class RemoveApplicationUserByIdCommand : IRequest
{
    public int Id { get; set; }
}