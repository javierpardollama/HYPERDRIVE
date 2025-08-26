using MediatR;

namespace Hyperdrive.Application.Commands.ApplicationRole;

public class RemoveApplicationRoleByIdCommand : IRequest
{
    public int Id { get; set; }
}