using Hyperdrive.Identity.Application.ViewModels.Updates;
using Hyperdrive.Identity.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Identity.Application.Commands.ApplicationRole;

public class UpdateApplicationRoleCommand : IRequest<ViewApplicationRole>
{
    public UpdateApplicationRole ViewModel { get; set; }
}