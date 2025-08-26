using Hyperdrive.Application.ViewModels.Updates;
using MediatR;

namespace Hyperdrive.Application.Commands.ApplicationRole;

public class UpdateApplicationRoleCommand : IRequest
{
    public UpdateApplicationRole ViewModel { get; set; }
}