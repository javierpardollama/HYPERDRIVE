using Hyperdrive.Main.Application.ViewModels.Updates;
using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Main.Application.Commands.ApplicationRole;

public class UpdateApplicationRoleCommand : IRequest<ViewApplicationRole>
{
    public UpdateApplicationRole ViewModel { get; set; }
}