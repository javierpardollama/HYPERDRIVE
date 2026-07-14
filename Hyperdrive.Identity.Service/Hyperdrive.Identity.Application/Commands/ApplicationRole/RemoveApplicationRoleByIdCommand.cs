using MediatR;

namespace Hyperdrive.Identity.Application.Commands.ApplicationRole;

public class RemoveApplicationRoleByIdCommand : IRequest
{
    public int Id { get; set; }
}