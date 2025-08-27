using Hyperdrive.Application.ViewModels.Additions;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Commands.ApplicationRole;

public class AddApplicationRoleCommand : IRequest<ViewApplicationRole>
{
    public AddApplicationRole ViewModel { get; set; }
}