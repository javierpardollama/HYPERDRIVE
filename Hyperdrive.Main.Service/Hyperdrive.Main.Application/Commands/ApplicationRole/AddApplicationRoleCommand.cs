using Hyperdrive.Main.Application.ViewModels.Additions;
using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Main.Application.Commands.ApplicationRole;

public class AddApplicationRoleCommand : IRequest<ViewApplicationRole>
{
    public AddApplicationRole ViewModel { get; set; }
}