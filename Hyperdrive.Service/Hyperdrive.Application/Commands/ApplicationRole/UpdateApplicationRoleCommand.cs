using Hyperdrive.Application.ViewModels.Updates;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Commands.ApplicationRole;

public class UpdateApplicationRoleCommand : IRequest<ViewApplicationUser>
{
    public UpdateApplicationRole ViewModel { get; set; }
}