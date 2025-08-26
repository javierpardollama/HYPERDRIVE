using Hyperdrive.Application.ViewModels.Additions;
using MediatR;

namespace Hyperdrive.Application.Commands.ApplicationRole;

public class AddApplicationRoleCommand : IRequest
{
    public AddApplicationRole ViewModel { get; set; }
}