using Hyperdrive.Application.ViewModels.Updates;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Commands.ApplicationRole;

public class UpdateApplicationRoleCommand : IRequest<ViewApplicationRole>
{
    public UpdateApplicationRole ViewModel { get; set; }
}