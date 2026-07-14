using Hyperdrive.Identity.Application.ViewModels.Updates;
using Hyperdrive.Identity.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Identity.Application.Commands.ApplicationUser;

public class UpdateApplicationUserCommand : IRequest<ViewApplicationUser>
{
    public UpdateApplicationUser ViewModel { get; set; }
}