using Hyperdrive.Identity.Application.ViewModels.Additions;
using Hyperdrive.Identity.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Identity.Application.Commands.ApplicationRole;

public class AddApplicationRoleCommand : IRequest<ViewApplicationRole>
{
    public AddApplicationRole ViewModel { get; set; }
}